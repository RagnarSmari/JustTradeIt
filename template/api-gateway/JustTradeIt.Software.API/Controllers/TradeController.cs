using System.Linq;
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

        // TODO: Setup routes
        [HttpGet, Route("")]
        public IActionResult GetAllTrades()
        {
            // TODO: Call the TradeService
            // TODO: Returns a list of all trades
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult PostTrade([FromBody] TradeInputModel trade)
        {
            // TODO: Requests a trade to a particular user.
            // Expecting that the first item in the 
            var userName = User.Claims.FirstOrDefault(c => c.Type == "FullName").Value;
            var name = _tradesService.CreateTradeRequest(userName, trade);
            return Ok();
        }

        [HttpGet, Route("{id:int}")]
        public IActionResult GetTrade(int id)
        {
            // Get a detailed version of a trade request
            // TODO: Call the TradeService
            // TODO: Returns a TradeDetailDto
            return Ok();
        }

        [HttpPut, Route("{id:int}")]
        public IActionResult PutTrade(int id)
        {
            // Updates the status of a trade request.
            // ONly a participant of the trade offering can update the status
            // of the trade request.
            // Although if the trade request is in a finalized start it cannot be altered.
            // The only non finalized state is the pending state
            return Ok();
        }
    }
}