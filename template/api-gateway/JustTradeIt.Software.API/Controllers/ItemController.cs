using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        // TODO: Setup routes
        [HttpGet, Route("")]
        public IActionResult GetAllItems()
        {
            // TODO: Call ItemService
            // TODO: Returns a list of all items
            // The result is an envelope containing the results in a page
            return Ok();
        }

        [HttpGet, Route("{id:int}")]
        public IActionResult GetItemById(int id)
        {
            // Gets a detailed version of an item
            return Ok();
        }

        [HttpPost, Route("")]
        public IActionResult NewITem()
        {
            // TODO: Call the ItemService
            // Create a new item which will be associated with the authenticated user
            // and other users will see the new item and can request a trade to acquire that item
            return Ok();
        }

        [HttpDelete, Route("")]
        public IActionResult DeleteITem()
        {
            // TODO: Call the ItemService
            // Delete an item from the inventory of the authenticated user.
            // The item should only be soft deleted from the database.
            // All trade requests which include the deleted item should be marked as cancelled
            return Ok();
        }
        
        
    }
}