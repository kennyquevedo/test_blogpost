using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Domain.Interfaces
{
    public interface IUserRepo : IGenericRepository<User>
    {
        User GetUser(int id);
        User ValidateUser(string userName, string password);
    }
}
