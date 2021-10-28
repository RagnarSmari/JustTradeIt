using System;
using System.Collections.Generic;
using System.Linq;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.Enums;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradesService;

        public TradeController(ITradeService tradesService)
        {
            _tradesService = tradesService;
        }
        
        [HttpGet, Route("")]
        public IActionResult GetAllTrades([FromQuery] bool onlyCompleted=false, [FromQuery] bool onlyIncludeActive=false)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            IEnumerable<TradeDto> allTrades;
            if (onlyCompleted && !onlyIncludeActive)
            {
                allTrades = _tradesService.GetTrades(email);
            }

            else if (onlyIncludeActive && !onlyCompleted)
            {
                allTrades = _tradesService.GetTradeRequests(email);
            }
            else if (onlyIncludeActive && onlyCompleted)
            {
                // Does not make sense to have both true my guys
                throw new Exception("Cannot have both true");
            }
            else
            {
                var comple = _tradesService.GetTrades(email);
                var active = _tradesService.GetTradeRequests(email);
                allTrades = comple.Concat(active);
            }
            return Ok(allTrades);
        }

        [HttpPost]
        [Route("")]
        public IActionResult PostTrade([FromBody] TradeInputModel trade)
        {
            var userName = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var name = _tradesService.CreateTradeRequest(userName, trade);
            return Ok(name);
        }

        [HttpGet, Route("{identifier}")]
        public IActionResult GetTrade(string identifier)
        {
            // Get a detailed version of a trade request
            var item = _tradesService.GetTradeByIdentifer(identifier);
            return Ok(item);
        }

        [HttpPatch, Route("{identifier}")]
        public IActionResult PutTrade(string identifier, [FromBody] string newStatus)
        {
            string name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            _tradesService.UpdateTradeRequest(identifier, name, newStatus);
            return NoContent();
        }
    }
}