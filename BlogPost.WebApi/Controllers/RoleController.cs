using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogPost.WebApi.Controllers
{
    /// <summary>
    /// Api controller for Role
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitWork"></param>
        public RoleController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        /// <summary>
        /// Get Role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = _unitWork.Roles.GetById(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _unitWork.Roles.GetAll();
            if (roles == null)
                return NotFound();

            return Ok(roles);

            //TODO: Validate empty/null objects.
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

            var role_ctx = new Role
            {
                Name = role.Name,
                CreatedDate = DateTime.UtcNow
            };

            _unitWork.Roles.Add(role_ctx);
            _unitWork.Complete();

            return Ok();

            //TODO: Add swagger documentation
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

            if (role.Id <= 0)
                return BadRequest("Id role must be provides to update the entity.");

            var role_up = _unitWork.Roles.GetById(role.Id);
            if (role_up != null)
            {
                role_up.Name = role.Name;
                role_up.ModifiedDate = DateTime.UtcNow;
            }
            else
            {
                return StatusCode(StatusCodes.Status304NotModified, "role not found");
            }

            _unitWork.Roles.Update(role_up);
            _unitWork.Complete();

            return Ok();
        }
    }
}
