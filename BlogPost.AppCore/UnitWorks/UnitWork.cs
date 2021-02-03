using BlogPost.AppCore.Repo;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.AppCore.UnitWorks
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationContext _context;
        public IRoleRepo Roles { get; private set; }
        public IUserRepo Users { get; private set; }
        public IUserRoleRepo UserRoles { get; private set; }

        public UnitWork(ApplicationContext context)
        {
            _context = context;

            Roles = new RoleRepo(_context);
            Users = new UserRepo(_context);
            UserRoles = new UserRoleRepo(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
