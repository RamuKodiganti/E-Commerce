using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using e_comm.Auth;
using e_comm.Models;
using e_comm.Services;
using E_Commerce.DTO;
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
            catch (UserNotFoundException ex)
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
        public IActionResult Register(User user)
        {
            try
            {
                var message = "User added successfully";
                // Hash the password before saving
                //user.Password = HashPassword(user.Password);
                //user.Password = HashPassword(user.Password);
                service.AddUser(user);
                return StatusCode(201, message);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{email}")]
        //[Authorize(Roles = "User")]
        //[Authorize(Roles = "Admmin")]
        [AllowAnonymous]

        public IActionResult Update(string email, UserDto userDto)
        {
            try
            {
                var message = "Password updated successfully";
                service.UpdateUser(email, userDto);
                return Ok(message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        [Authorize(Roles = "Admmin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var message = $"Delete UserId:{id} successfully";
                service.DeleteUser(id);
                return Ok(message);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Authentication([FromBody] UserCredentials user)
        {
            try
            {
                var token = auth.Authentication(user.Email, user.Password);
                if (token == null)
                {
                    return Unauthorized(new { error = "Invalid email or password" });
                }
                return Ok(token);
                //return Ok(new { token, role = user.Role });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //private string HashPassword(string password)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        var builder = new StringBuilder();
        //        foreach (var b in bytes)
        //        {
        //            builder.Append(b.ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}
    }
}