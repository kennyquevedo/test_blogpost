using System;

namespace BlogPost.Domain.Interfaces
{
    public interface IUnitWork:IDisposable
    {
        IRoleRepo Roles { get; }
        int Complete();

        //TODO:Remove unused spacename
    }
}
