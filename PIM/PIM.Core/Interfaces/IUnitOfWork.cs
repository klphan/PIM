using System;
using PIM.Core.Entities;

namespace PIM.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Project { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Group> Group { get; }

        int Commit();
    }
}
