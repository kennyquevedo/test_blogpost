using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Domain.Interfaces
{
    public interface IRoleRepo:IGenericRepository<Role>
    {
        IEnumerable<Role> GetMostUsedRoles();
    }
}
