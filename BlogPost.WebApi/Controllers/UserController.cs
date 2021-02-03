using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.WebApi.Controllers
{
    /// <summary>
    /// Api controller for user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUnitWork _unitWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitWork"></param>
        public UserController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        /// <summary>
        /// Add new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddUser([FromBody] Dto.User user)
        {
            if (user == null)
                return BadRequest("Value must be passed in the request body");

            var user_ctx = new User
            {
                Name = user.Name,
                UserName = user.UserName,
                CreatedDate = DateTime.UtcNow,
                Password = user.Password
            };

            _unitWork.Users.Add(user_ctx);

            try
            {
                _unitWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);

                //TODO: add catchs for exceptions.
            }

        }

        /// <summary>
        /// Update user info.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateUser([FromBody] Dto.User user)
        {
            if (user == null)
                return BadRequest("Value must be passed in the request body");


            var user_upd = _unitWork.Users.GetById(user.Id);
            if (user_upd != null)
            {
                user_upd.Name = user.Name;
                user_upd.ModifiedDate = DateTime.UtcNow;
                user_upd.Password = user.Password;
                user_upd.UserName = user.UserName;
            }
            else
            {
                return NotFound();
            }

            try
            {
                _unitWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _unitWork.Users.GetById(id);
                if (user == null)
                    return NotFound();

                _unitWork.Users.Remove(user);
                _unitWork.Complete();

                return Ok();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);

                //TODO: order methods
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _unitWork.Users.GetAll();
                if (users == null || users.Count() <= 0)
                    return NotFound();

                return Ok(users);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _unitWork.Users.GetById(id);
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);

                //TODO: order methods
            }
        }

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("username/{userName}")]
        public IActionResult GetUserByUserName(string userName)
        {
            try
            {
                var user = _unitWork.Users.Find(u => u.UserName == userName).FirstOrDefault();
                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);

                //TODO: order methods
            }
        }
        /// <summary>
        /// Get user by user name and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("validate")]
        public IActionResult GetUserByUserNameAndPassword([FromBody] Dto.User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();

                var user_ctx = _unitWork.Users.Find(u => u.UserName == user.UserName
                    && u.Password == user.Password)
                    .FirstOrDefault();

                if (user_ctx == null)
                    return NotFound();

                return Ok(Guid.NewGuid());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }
    }
}
