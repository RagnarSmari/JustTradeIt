using System;
using System.Linq;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        // TODO: Setup routes
        

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] RegisterInputModel register)
        {
            // Creates a new user
            var user = _accountService.CreateUser(register);
            return Ok(user);
        }
        
        
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult LogIn([FromBody] LoginInputModel login)
        {
            var user = _accountService.AuthenticateUser(login);
            if (user == null)
            {
                return Unauthorized(); 
            }
            
            return Ok(_tokenService.GenerateJwtToken(user));
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            // TODO: Retrieve token id from claim and blacklist token
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            Console.WriteLine(tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }
        
        [HttpGet, Route("profile")]
        public IActionResult GetProfile()
        {

            var name = User.Claims.FirstOrDefault(c => c.Type == "FullName").Value;
            Console.WriteLine(name);
            var user = _accountService.GetProfileInformation(name);
            return Ok(user);
        }

        [HttpPut, Route("profile")]
        public IActionResult UpdateProfile([FromBody] ProfileInputModel profile)
        {
            // TODO: Call the authenticationService
            var email = User.Claims.FirstOrDefault(c => c.Type == "FullName").Value;
            Console.WriteLine(email);
            _accountService.UpdateProfile(email, profile);
            return NoContent();
        }
    }
}
