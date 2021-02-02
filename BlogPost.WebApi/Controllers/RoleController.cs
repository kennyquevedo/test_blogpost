using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitWork _unitWork;

        public RoleController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }

        public IActionResult GetMostUsedRoles()
        {
            var roles = _unitWork.Roles.GetMostUsedRoles();
            return Ok(roles);

            //TODO: Validate empty/null objects.
        }

        [HttpPost]
        public IActionResult AddRole()
        {
            var role = new Role
            {
                Name = "role_" + DateTime.Now.ToString("yyyyMMdd_HHmmss")
            };

            _unitWork.Roles.Add(role);
            _unitWork.Complete();

            return Ok();
        }
    }
}
