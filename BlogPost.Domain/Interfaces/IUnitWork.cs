using System;

namespace BlogPost.Domain.Interfaces
{
    public interface IUnitWork : IDisposable
    {
        IRoleRepo Roles { get; }
        IPostRepo Posts { get; }
        int Complete();

        //TODO: Remove unused spacename
    }
}
