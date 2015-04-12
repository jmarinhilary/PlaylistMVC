using MyTunes.Dominio;
using System;
using System.Linq;
namespace MyTunes.Repository
{
    public interface IRepository<T> : IDisposable
           where T:EntityBase
    {
        void Create(T entity);
        IQueryable<T> Get();
        void Update(T entity);
    }
}
