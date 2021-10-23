using JustTradeIt.Software.API.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // TODO: Setup routes
        [HttpGet]
        [Route("/{identifier}")]
        public IActionResult GetUserInformation(string identifier)
        {
            
            // Get a user profile information
            // TODO: Call the UserService 
            
            return Ok();
        }
        
        [HttpGet]
        [Route("/{identifier}/trades")]
        public IActionResult GetUserTrades(string identifier)
        {
            // Get all successful trades associated with a user
            // TODO: Call the UserService 
            return Ok();
        }
    }
}