using System;
using MyTunes.Dominio;
using MyTunes.Repository;

namespace MyTunes.Services.Entities
{
    public class CustomerService : IDisposable
    {
        private CustomerRepository _customerRepository;
        private ChinookContext _chinookContext;
        public CustomerService()
        {
            _chinookContext = new ChinookContext();
            _customerRepository = new CustomerRepository(_chinookContext);
        }
        public void Create(Common.ViewModels.CustomerViewModel customerViewModel)
        {
            // customerViewModel -> Customer
            var customer = new Customer
            {
                FirstName = customerViewModel.FirstName,
                LastName = customerViewModel.LastName,
                Email = customerViewModel.Email
            };
            // Tratamiento de Errores, traducir el error para UI
            _customerRepository.Create(customer);
            
        }

        public void Dispose() 
        {
            _chinookContext = null;
            _customerRepository = null;
        }
    }
}
