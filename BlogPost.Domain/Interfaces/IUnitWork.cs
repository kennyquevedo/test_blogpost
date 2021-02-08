using System;

namespace BlogPost.Domain.Interfaces
{
    public interface IUnitWork : IDisposable
    {
        IPostRepo Posts { get; }
        int Complete();

        //TODO: Remove unused spacename
    }
}
