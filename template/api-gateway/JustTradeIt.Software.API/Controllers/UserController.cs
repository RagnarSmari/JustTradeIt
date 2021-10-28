using JustTradeIt.Software.API.Services.Implementations;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Route("{identifier}")]
        public IActionResult GetUserInformation(string identifier)
        {
            var user = _userService.GetUserInformation(identifier);
            return Ok(user);
        }
        
        [HttpGet]
        [Route("{identifier}/trades")]
        public IActionResult GetUserTrades(string identifier)
        {
            var trades = _userService.GetUserTrades(identifier);
            return Ok(trades);
        }
    }
}