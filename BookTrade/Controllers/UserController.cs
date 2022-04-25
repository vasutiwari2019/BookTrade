using BookTrade.BookTradeData;
using BookTrade.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookTrade.Controllers
{
    // UserController for handling User API calls
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Global Variables
        private readonly IUserData _userData;
        #endregion

        #region Constructor
        public UserController(IUserData userData)
        {
            _userData = userData;
        }
        #endregion

        #region Public Methods

        // API for getting all users
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetUsers()
        {
            return Ok(_userData.GetUsers());
        }

        // API for getting a specified user
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetUsers(Guid id)
        {
            var user = _userData.GetUsers(id);
            if (user != null)
                return Ok(user);
            else
                return NotFound($"User with Id:{id} was not found");
        }

        // API for adding a new user
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddUsers(User user)
        {
            var usercreated = _userData.AddUser(user);
            if (usercreated != null)
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.UserId, user);
            else
                return BadRequest("email id already exists");
        }

        // API for deleting a specified user
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteUsers(Guid id)
        {
            var existingUser = _userData.GetUsers(id);

            if (existingUser != null)
            {
                _userData.DeleteUser(existingUser);
                return Ok("User deleted");
            }

            return NotFound($"User with Id:{id} was not found");
        }

        // API for editing a specified user
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditUsers(Guid id, User user)
        {
            var existingUser = _userData.GetUsers(id);

            if (existingUser != null)
            {
                user.UserId = existingUser.UserId;
                _userData.EditUser(user);
                return Ok(user);
            }

            return NotFound($"User with Id:{id} was not found");
        }

        // API for finding user login details
        [HttpPost]
        [Route("api/[controller]/find")]
        public IActionResult FindUsers(Login user)
        {
            var userFound = _userData.LoginUser(user);

            if(userFound == null)
                return NotFound("Login details not found");

            else if (userFound.Email != null)
            {
                return Ok(userFound);
            }

            else
            {
                return BadRequest("Wrong Password");
            }
        }

        // API for updating user password
        [HttpPatch]
        [Route("api/[controller]/updatePassword")]
        public IActionResult UpdateUserPassword(Login user)
        {
            var userFound = _userData.UpdateUserPassword(user);

            if (userFound != null && userFound?.Password!=null)
            {
                return Ok(userFound);
            }

            else if(userFound?.Password == null)
            {
                return BadRequest("Same Password");
            }

            else
            {
                return NotFound("user password not updated");
            }
        }
        #endregion
    }
}
