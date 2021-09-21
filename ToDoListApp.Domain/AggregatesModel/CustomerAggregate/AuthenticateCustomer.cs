
using System.Threading.Tasks;
using ToDoListApp.Domain.AggregatesModel.CustomerAggregate;

namespace ToDoListApp.Domain.AggregatesModel.CustomerAggregate
{
    public class AuthenticateCustomer
    {
        private readonly ICustomerRepository _customerRepository;

        public AuthenticateCustomer(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }

        public async Task<Customer> Login(string name, string pass)
        {
            return await _customerRepository.AuthenticateCustomerAsync(name, pass);
        }
    }
}
