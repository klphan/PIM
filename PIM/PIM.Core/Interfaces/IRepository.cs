using System;
using System.Linq;
using PIM.Core.Entities;

namespace PIM.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Add(T a);
        void Remove(Guid id);
        IQueryable<T> Get();
        T FindById(Guid id);
    }
}

