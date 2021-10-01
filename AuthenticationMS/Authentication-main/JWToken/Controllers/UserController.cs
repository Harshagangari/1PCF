using JWToken.Data;
using JWToken.Model;
using JWToken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        [AllowAnonymous]
        //[HttpPost("Login")]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
            nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public IActionResult giveToken(int id)
        {
           string password = new Random().Next(12345, 98765).ToString();
            string pass = _userService.Login(id,password);
            if(pass.Length<100)
            {
                return BadRequest(pass);
            }
            else
            {
                Response.Cookies.Append("token", pass, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true
                });
                return Ok(pass);
            } 
        }

        // POST api/user/Signup
        ///<summary>
        ///Sign up
        /// </summary>
        /// <return>
        /// Sign up status
        /// </return>
        /// <remarks>
        /// Sample request
        /// POST / api/User
        /// 
        /// </remarks>
        /// <response code="201">Signed up successfully</response>
        [AllowAnonymous]
        [HttpPost("SignUp")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]

        public IActionResult SignUp([FromBody] User user)
        {
            string pass = _userService.UserSignUp(user);
            if(pass== "Successfully Registered")
            {
                return Ok(pass);
            }
            else
            {
                return BadRequest(pass);
            }
        }


        /// <summary>
        /// Get User Data
        /// </summary>
        /// <return>
        /// Returns User Data
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/User
        /// 
        /// </remarks>
        /// <response code="200">User Data</response>

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult<User> UserData(int id)
        {
            User user = _userService.UserDetails(id);
            return user;

        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <return>
        /// Logs out
        /// </return>
        /// <remarks>
        /// Sample request
        /// GET /api/User
        /// 
        /// </remarks>
        /// <response code="200">Successfully logged out</response>
        [HttpGet("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return Ok();

        }

        
    }
}
