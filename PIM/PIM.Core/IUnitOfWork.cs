using PIM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIM.Core
{

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Project> Project { get; }
        IRepository<Employee> Employee { get; }
        IRepository<Group> Group { get; }

        int Commit();
    }

}
