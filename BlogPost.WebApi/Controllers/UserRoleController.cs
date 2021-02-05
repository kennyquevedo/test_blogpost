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
    /// Represent relationship between roles and users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        //IUnitWork _unitWork;

        ///// <summary>
        ///// Ctor
        ///// </summary>
        ///// <param name="unitWork"></param>
        //public UserRoleController(IUnitWork unitWork)
        //{
        //    _unitWork = unitWork;
        //}

        //#region Post

        ///// <summary>
        ///// Add roles to the user.
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult AddUserToRole([FromBody] Dto.User user)
        //{
        //    //TODO: region for get and post

        //    if (user == null)
        //        return BadRequest("Please provide the user value.");

        //    if (user.Roles == null || user.Roles.Count <= 0)
        //        return BadRequest("Please provide the role list");

        //    //Get user
        //    var user_ctx = _unitWork.Users.GetById(user.Id);
        //    if (user_ctx == null)
        //        return NotFound("User not found.");

        //    //Get roles
        //    var roles_ctx = _unitWork.Roles.GetRolesByIds(user.Roles.Select(r => r.Id)).ToList();
        //    if (roles_ctx == null && roles_ctx.Count() <= 0)
        //        return NotFound("Roles not found.");

        //    //Add roles to user.
        //    var userRoles = new List<UserRole>();
        //    roles_ctx.ForEach(r =>
        //    {
        //        //If not exists the user with the role, save it.
        //        var userRole = _unitWork.UserRoles.Find(ur => ur.UserId == user.Id 
        //        && ur.RoleId == r.Id).FirstOrDefault();

        //        if (userRole == null)
        //        {
        //            userRoles.Add(new UserRole()
        //            {
        //                Role = r,
        //                RoleId = r.Id,
        //                User = user_ctx
        //            });
        //        }
        //    });

        //    try
        //    {
        //        _unitWork.UserRoles.AddRange(userRoles);
        //        _unitWork.Complete();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //           (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
        //    }
        //}

        //#endregion

        //#region Get

        //#endregion
    }
}
