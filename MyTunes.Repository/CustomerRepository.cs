using MyTunes.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MyTunes.Repository
{
    public class CustomerRepository : Repository<Customer, ChinookContext>
    {
        //private ChinookContext _context;
        public CustomerRepository(ChinookContext context)
            : base(context)
        {
            //_context = (ChinookContext)context;
        }

        

        //public IQueryable<Customer> Get()
        //{
        //    return _context.Customer.AsQueryable();
        //}

        //public Customer GetByEmail(string userName)
        //{
        //    return _context.Customer.FirstOrDefault(x => x.Email == userName);
        //}

        //public void Dispose()
        //{
        //    _context = null;
        //}

        //public void Create(Customer customer)
        //{
        //    _context.Customer.Add(customer);
        //    _context.SaveChanges();
        //}

        //void IRepository<Customer>.Create(Customer playList)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(Customer playList)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
