using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.CustomerAggregate;

namespace Todo.Domain.Services
{
    public class AuthenticateUser
    {
        private readonly ICustomerRepository _CustomerRepository;

        public AuthenticateUser(ICustomerRepository userRepository)
        {
            this._CustomerRepository = userRepository;
        }

        public async Task<Customer> Login(string name, string pass)
        {
            return await _CustomerRepository.AuthenticateUserAsync(name, pass);
        }
    }
}
