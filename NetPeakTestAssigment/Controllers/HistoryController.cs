using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPeakTestAssigment.Models;
using NetPeakTestAssigment.Sockets;

namespace NetPeakTestAssigment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        IParsedSitesRepository _parsedRepository;
        private readonly NotificationSocketManager _socketManager;
        public HistoryController(IParsedSitesRepository parsedRepository,
                                 NotificationSocketManager socketManager)
        {
            _parsedRepository = parsedRepository;
            _socketManager = socketManager;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_parsedRepository.GetAll().Select(i =>
            new
            {
                Id = i.Id,
                Uri = i.Uri
            }).OrderByDescending(i => i.Id));
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(_parsedRepository.GetById(id));
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            _parsedRepository.DeleteById(id);

            //Notify client to update history
            await _socketManager.SendMessageToAllAsync(string.Format("{{event: Deleted, id: {0} }}", id));
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Add([FromBody]ParsedSiteDTO data)
        {
            if (data == null)
                return BadRequest("Invalid argument exception");
            _parsedRepository.Add(data);

            //Notify client to update history
            await _socketManager.SendMessageToAllAsync(string.Format("{{event: ItemAdded, id: {0} }}", data.Id));
            return Created($"/api/history/{data.Id}", data);
        }
    }
}