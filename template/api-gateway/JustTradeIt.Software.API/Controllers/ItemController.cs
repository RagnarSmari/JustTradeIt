using JustTradeIt.Software.API.Services.Interfaces;
using JustTradeIt.Software.API.Models;
using Microsoft.AspNetCore.Mvc;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using System.Linq;

namespace JustTradeIt.Software.API.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService){
            _itemService = itemService;
        }
        // TODO: Setup routes
        [HttpGet, Route("")]
        public IActionResult GetAllItems(int pageNumber=1,int pageSize=1, int MaxPages=5 ,bool order=true)
        {
            // TODO: Call ItemService
            // TODO: Returns a list of all items
            // The result is an envelope containing the results in a page
            var allItems = _itemService.GetItems(pageNumber,pageSize, MaxPages, order );
            return Ok(allItems);
        }

        [HttpGet, Route("{identifier}")]
        public IActionResult GetItemById(string identifier)
        {
            // Gets a detailed version of an item
            var item = _itemService.GetItemByIdentifier(identifier);
            return Ok(item);
        }

        [HttpPost, Route("")]
        public IActionResult NewITem([FromBody] ItemInputModel model)
        {
            // TODO: Call the ItemService
            // Create a new item which will be associated with the authenticated user
            // and other users will see the new item and can request a trade to acquire that item
            var email = User.Claims.FirstOrDefault(c => c.Type == "Email").Value;
            var item = _itemService.AddNewItem(email, model);
            return Ok(item);
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