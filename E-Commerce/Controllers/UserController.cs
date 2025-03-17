using e_comm.Auth;
using e_comm.Models;
using e_comm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserWebAPI.Exceptions;
using UserWebAPI.Exceptions.DemoAPI.Exception;

namespace e_comm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly IAuth auth;

        public UserController(IAuth auth, IUserService service)
        {
            this.auth = auth;
            this.service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(service.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                return Ok(service.GetUser(id));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser(User user)
        {
            try
            {
                return StatusCode(201, service.AddUser(user));
            }
            catch (CustomerAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User user)
        {
            try
            {
                return Ok(service.UpdateUser(id, user));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(service.DeleteUser(id));
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredentials user)
        {
            try
            {
                var token = auth.Authentication(user.Email, user.Password);
                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}