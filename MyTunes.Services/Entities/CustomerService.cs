using System;
using MyTunes.Dominio;
using MyTunes.Repository;

namespace MyTunes.Services.Entities
{
    public class CustomerService : IDisposable
    {
        private IRepository<Customer> _customerRepository;


        public CustomerService(IRepository<Customer> customerRepository)
        {
            this._customerRepository = customerRepository;
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
            _customerRepository = null;
        }
    }
}
