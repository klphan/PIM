using System;
using PIM.Core;
using PIM.Core.Interfaces;

namespace PIM.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PIMContext _context;

        public UnitOfWork(PIMContext context)
        {
            _context = context;
            Project = new Repository<Project>(_context);
            Group = new Repository<Group>(_context);
            Employee = new Repository<Employee>(_context);
            ProjectEmployee = new Repository<ProjectEmployee>(_context);

        }

        public IRepository<Project> Project { get; private set; }
        public IRepository<Employee> Employee { get; private set; }
        public IRepository<Group> Group { get; private set; }
        public IRepository<ProjectEmployee> ProjectEmployee { get; private set; }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        
    }
}
