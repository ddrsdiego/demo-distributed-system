using System;
using System.Threading.Tasks;
using ThinkerThings.Service.CustomerManagement.Domain.AggregateModels.CustomerAggregate;

namespace ThinkerThings.Service.CustomerManagement.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public CustomerRepository()
        {
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            await Task.CompletedTask;
            return null;
        }

        public async Task Register(Customer newCustomer)
        {
            await Task.CompletedTask;
        }

        public async Task Update(Customer newCustomer)
        {
            await Task.CompletedTask;
        }
    }
}