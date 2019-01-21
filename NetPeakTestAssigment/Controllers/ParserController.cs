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
namespace NetPeakTestAssigment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IHttpClient _httpRequest;
        private readonly IHtmlParser _parser;
        public ParserController(IHttpClient httpRequest,
                                IHtmlParser parser)
        {
            _httpRequest = httpRequest;
            _parser = parser;
        }

        [HttpGet("html")]
        public async Task<ActionResult> ParsePage(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("Invalid Argument Exception. Please, provide a link in the query string");

            ResponseHttp responseHttp = await _httpRequest.SendAsync(url);


            SiteDetails parsingResult = _parser.ParseHTML(responseHttp.Body);


            ParsedSiteDTO result = new ParsedSiteDTO
            {
                Title = parsingResult.Title,
                Description = parsingResult.Description,
                Headers = parsingResult.HeadersH1,
                Images = parsingResult.Images.BeautifyImageLinks(responseHttp.ResponseUri),
                Links = parsingResult.Links.BeautifyLinks(responseHttp.ResponseUri),

                ResponseTime = responseHttp.TimeToFirstByte,
                ServerResponse = new ServerResponseDTO
                {
                    Code = (int)responseHttp.Status.Code,
                    Description = responseHttp.Status.Description
                }
            };

            return Ok(result);

        }
    }
}