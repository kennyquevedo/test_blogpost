using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.AppCore.Repo
{
    public class UserRoleRepo : GenericRepo<UserRole>, IUserRoleRepo
    {
        public UserRoleRepo(ApplicationContext context) : base(context)
        {

        }
    }
}
