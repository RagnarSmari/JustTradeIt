using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        // TODO: Setup routes

        [AllowAnonymous]
        [HttpPost, Route("/register")]
        public ActionResult Register([FromBody] RegisterInputModel register)
        {
            return NoContent();
        }
        
        
        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult LogIn([FromBody] LoginInputModel login)
        {
            // TODO: Call a authenticationService
            // TODO: Return valid JWT Token
            return Ok();
        }

        [HttpGet, Route("/logout")]
        public IActionResult SignOut()
        {
            // TODO: Retrieve token id from claim and blacklist token
            return NoContent();
        }
        
        [HttpGet, Route("/profile")]
        public IActionResult GetProfile()
        {
            var claims = User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
            return Ok(claims);
        }

        [HttpPut, Route("/profile")]
        public IActionResult UpdateProfile([FromBody] ProfileInputModel profile)
        {
            // TODO: Call the authenticationService
            return NoContent();
        }
    }
}
