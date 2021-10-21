using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // TODO: Setup routes
        [HttpGet, Route("/{id:int]")]
        public IActionResult GetUserInformation(int id)
        {
            // Get a user profile information
            // TODO: Call the UserService 
            return Ok();
        }

        [HttpGet, Route("/{id:int}/trades")]
        public IActionResult GetUserTrades(int id)
        {
            // Get all successful trades associated with a user
            // TODO: Call the UserService 
            return Ok();
        }
    }
}