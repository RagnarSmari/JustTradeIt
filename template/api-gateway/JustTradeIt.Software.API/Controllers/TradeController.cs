using JustTradeIt.Software.API.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        // TODO: Setup routes
        [HttpGet, Route("")]
        public IActionResult GetAllTrades()
        {
            // TODO: Call the TradeService
            // TODO: Returns a list of all trades
            return Ok();
        }

        [HttpPost, Route("")]
        public IActionResult PostTrade([FromBody] TradeInputModel trade)
        {
            // TODO: Requests a trade to a particular user.
            // Trade Proposal always includes at least one item from each participant. 
            // Therefore if you want to acquire a certain item, you must offer some of your
            // items which you believe are equally valuable as the desired item
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