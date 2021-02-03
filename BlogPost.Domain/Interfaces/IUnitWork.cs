using System;

namespace BlogPost.Domain.Interfaces
{
    public interface IUnitWork : IDisposable
    {
        IRoleRepo Roles { get; }
        IUserRepo Users { get; }
        IUserRoleRepo UserRoles { get; }
        int Complete();

        //TODO: Remove unused spacename
    }
}
