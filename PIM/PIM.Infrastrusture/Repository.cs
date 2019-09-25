using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIM.Core;
using PIM.Core.Interfaces;
using System.Data.Entity;

namespace PIM.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        //protected field for later use in Project and Group 
        protected readonly DbContext context;
        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Remove(Guid id)
        {
            T entity = FindById(id); 
            context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return context.Set<T>();
        }

        public T FindById(Guid id)
        {
            return context.Set<T>().Find(id);
        }
    }

}
