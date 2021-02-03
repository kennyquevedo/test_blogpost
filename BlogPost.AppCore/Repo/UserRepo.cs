using BlogPost.Domain;
using BlogPost.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.AppCore.Repo
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(ApplicationContext context) : base(context)
        {

        }
    }
}
