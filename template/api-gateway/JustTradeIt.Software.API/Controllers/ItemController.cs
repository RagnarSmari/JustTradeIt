using System;
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
        [HttpGet, Route("")]
        public IActionResult GetAllItems(int pageNumber=1,int pageSize=1, int MaxPages=5 , [FromQuery] bool ascendingSortOrder=true)
        {
            var allItems = _itemService.GetItems(pageNumber,pageSize, MaxPages, ascendingSortOrder );
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
            var email = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            var item = _itemService.AddNewItem(email, model);
            return Ok(item);
        }

        [HttpDelete, Route("{identifier}")]
        public IActionResult DeleteITem(string identifier)
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == "name").Value;
            _itemService.RemoveItem(name, identifier);
            return Ok();
        }
        
        
    }
}