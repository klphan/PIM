using System;
using System.Linq;
using PIM.Core.Interfaces;
using System.Data.Entity;
using PIM.Core.Entities;

namespace PIM.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        //protected field for later use in Project and Group 
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            this.Context = context;
        }
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Remove(Guid id)
        {
            T entity = FindById(id); 
            Context.Set<T>().Remove(entity);
        }
        public IQueryable<T> Get()
        {
            return Context.Set<T>();
        }

        public T FindById(Guid id)
        {
            return Context.Set<T>().Find(id);
        }
    }

}
