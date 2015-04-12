using MyTunes.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes.Repository
{
    public abstract class Repository<TEntity, TContext> : IRepository<TEntity> 
        where TEntity : EntityBase
        where TContext : DbContext
    {
        protected TContext Context;
        
        public Repository(TContext context)
        {
            Context = context;
        }

        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context = null;
        }
    }
}
