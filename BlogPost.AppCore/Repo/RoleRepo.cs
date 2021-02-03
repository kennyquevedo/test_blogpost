using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogPost.AppCore.Repo
{
    public class RoleRepo : GenericRepo<Role>, IRoleRepo
    {
        public RoleRepo(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Role> GetMostUsedRoles()
        {
            return _context.Roles;
        }

        public IEnumerable<Role> GetRolesByIds(IEnumerable<int> ids)
        {
            return _context.Roles.Where(r => ids.Contains(r.Id));
        }
    }
}
