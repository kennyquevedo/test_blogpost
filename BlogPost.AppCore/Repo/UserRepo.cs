using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogPost.AppCore.Repo
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(ApplicationContext context) : base(context)
        {

        }

        public User GetUser(int id)
        {
            return _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.UserRoles)
                .FirstOrDefault();
        }

        public User ValidateUser(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
