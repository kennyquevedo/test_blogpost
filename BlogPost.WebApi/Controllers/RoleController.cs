using BlogPost.BLogic.Interfaces;
using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Dto = BlogPost.BLogic.Dto;
using BlogPost.Common;

namespace BlogPost.WebApi.Controllers
{
    /// <summary>
    /// Api controller for Role
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IBLRoles _blRoles;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="blRoles"></param>
        public RoleController(IBLRoles blRoles)
        {
            _blRoles = blRoles;
        }

        /// <summary>
        /// Add a role to database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddRole([FromBody] Dto.Role role)
        {
            if (role == null)
                return BadRequest("Value must be passed in the request body");

            try
            {
                _blRoles.AddRole(role);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateRole([FromBody] Dto.Role role)
        {
            if (role == null)
                return BadRequest("Value must be passed in the request body");

            try
            {
                //Update
                _blRoles.UpdateRole(role);
                return NoContent();
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete Role.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                _blRoles.DeleteRole(id);
                return NoContent();
            }
            catch (RecordNotFoundException recordEx)
            {
                return NotFound(recordEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("roles")]
        public IActionResult GetAll()
        {
            //TODO: Validate produceresponsetype attributes.
            //TODO: Validate 204 vs 404
            try
            {
                var roles = _blRoles.GetAll();
                if (roles != null)
                    return Ok(roles);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var role = _blRoles.GetById(id);

                if (role != null)
                    return Ok(role);
                else
                    return NoContent();
            }
            catch (RecordNotFoundException recordEx)
            {
                return NotFound(recordEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Get Role by Name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet("rolename")]
        public IActionResult GetRoleByName([FromBody] string roleName)
        {
            try
            {
                var role = _blRoles.GetByName(roleName);

                if (role != null)
                    return Ok(role);
                else
                    return NoContent();
            }
            catch (RecordNotFoundException recordEx)
            {
                return NotFound(recordEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
