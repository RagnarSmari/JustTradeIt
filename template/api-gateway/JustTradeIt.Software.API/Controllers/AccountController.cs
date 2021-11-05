using System;
using System.Linq;
using JustTradeIt.Software.API.Models.Exceptions;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JustTradeIt.Software.API.Controllers;

namespace JustTradeIt.Software.API.Controllers
{
    [ApiController]
    [Authorize]
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
        

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] RegisterInputModel register)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException();
            }
            // Creates a new user
            var user = _accountService.CreateUser(register);
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(token);
        }
        
        
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult LogIn([FromBody] LoginInputModel login)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException();
            }
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
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return NoContent();
        }
        
        [HttpGet, Route("profile", Name = "GetProfileInformation")]
        public IActionResult GetProfile()
        {

            var name = User.Identity.Name;
            var user = _accountService.GetProfileInformation(name);
            return Ok(user);
        }

        [HttpPut, Route("profile")]
        public IActionResult UpdateProfile([FromForm] ProfileInputModel profile)
        {
            if (!ModelState.IsValid)
            {
                throw new ModelFormatException();
            }
            var email = User.Identity.Name;
            _accountService.UpdateProfile(email, profile);
            return NoContent();
        }
    }
}
