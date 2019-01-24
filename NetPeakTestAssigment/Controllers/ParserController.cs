using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPeakTestAssigment.Models;
using NetPeakTestAssigment.Models.HttpClients;
using NetPeakTestAssigment.Models.Parser;
using NetPeakTestAssigment.Sockets;
using Newtonsoft.Json;

namespace NetPeakTestAssigment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IHttpClient _httpRequest;
        private readonly IHtmlParser _parser;
        private readonly IParsedSitesRepository _parsedRepository;
        private readonly NotificationSocketManager _socketManager;

        public ParserController(IHttpClient httpRequest,
                                IHtmlParser parser,
                                IParsedSitesRepository parsedRepository,
                                NotificationSocketManager socketManager)
        {
            _httpRequest = httpRequest;
            _parser = parser;
            _parsedRepository = parsedRepository;
            _socketManager = socketManager;
        }
        //
        //returns ServerResponseDTO object from specific url
        [HttpGet("html")]
        public async Task<ActionResult> ParsePage(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("Invalid Argument Exception. Please, provide a link in the query string");

            ResponseHttp responseHttp;
            Uri uri = new UriBuilder(url).Uri;
            try
            {
                responseHttp = await _httpRequest.SendAsync(uri);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest("The site cannot be reached");
            }
            SiteDetails parsingResult;
            try
            {
                parsingResult = _parser.ParseHTML(responseHttp.Body);
            }
            catch (Exception ex)
            {
                return BadRequest("Error was occurred when the site was parsed");
            }

            ParsedSiteDTO result = new ParsedSiteDTO
            {
                Uri = uri.AbsoluteUri,
                Title = parsingResult.Title,
                Description = parsingResult.Description,
                HeadersH1 = parsingResult.HeadersH1,
                Images = parsingResult.Images.imageLinksToAbsoluteLinks(responseHttp.ResponseUri),
                Links = parsingResult.Links.RelativeToAbsoluteLinks(responseHttp.ResponseUri),

                ResponseTime = responseHttp.TimeToFirstByte,
                ServerResponse = new ServerResponseDTO
                {
                    Code = (int)responseHttp.Status.Code,
                    Description = responseHttp.Status.Description
                }
            };

            _parsedRepository.Add(result);
            //Notify client to update history
            await _socketManager.SendMessageToAllAsync(string.Format("{{event: ItemAdded, id: {0} }}", result.Id));
            return Ok(result);

        }
    }
}