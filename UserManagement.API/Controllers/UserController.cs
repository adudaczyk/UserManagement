using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using UserManagement.BusinessLogic.Interfaces;
using UserManagement.BusinessLogic.Models;
using UserManagement.BusinessLogic.Models.Requests;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateService _authenticateService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService, 
            IAuthenticateService authenticateService, 
            ILogger<UserController> logger)
        {
            _userService = userService;
            _authenticateService = authenticateService;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate(AuthenticateRequest credentials)
        {
            var response = _authenticateService.Authenticate(credentials);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<UserDto>> GetUser(int id)
        {
            try
            {
                return Ok(_userService.GetUser(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddUser([FromBody] UserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                _userService.AddUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public ActionResult UpdateUser([FromBody] UserDto userDto)
        {
            try
            {
                _userService.UpdateUser(userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}