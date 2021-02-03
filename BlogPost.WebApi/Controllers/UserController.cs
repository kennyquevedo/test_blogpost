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

        #region Post


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

        #endregion

        #region Get
        /// <summary>
        /// Get all users
        /// </summary>
        [HttpGet("users")]
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
        [HttpGet("username")]
        public IActionResult GetUserByUserName([FromBody] string userName)
        {
            //TODO: replace route by verb
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
        [HttpGet("validate")]
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

                return Ok(user_ctx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Get user info and roles of the user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("userinfo/{id}")]
        public IActionResult GetUser(int id)
        {
            try
            {
                var user_ctx = _unitWork.Users.GetUser(id);
                if (user_ctx == null)
                    return NotFound();

                var roles = new List<Dto.Role>();
                foreach (var role in user_ctx.UserRoles)
                {
                    var role_db = new Role();
                    if (role.Role == null)
                        role_db = _unitWork.Roles.GetById(role.RoleId);
                    else
                        role_db = role.Role;

                    if (role.Role != null)
                    {
                        roles.Add(new Dto.Role()
                        {
                            CreatedDate = role_db.CreatedDate,
                            Id = role_db.Id,
                            ModifiedDate = role_db.ModifiedDate,
                            Name = role_db.Name
                        });
                    }
                }


                var userinfo = new Dto.User()
                {
                    Id = user_ctx.Id,
                    UserName = user_ctx.UserName,
                    CreatedDate = user_ctx.CreatedDate,
                    ModifiedDate = user_ctx.ModifiedDate,
                    Name = user_ctx.Name,
                    Password = user_ctx.Password,
                    Roles = roles
                };

                return Ok(userinfo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError,
                    (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
            }
        }

        #endregion



    }
}
