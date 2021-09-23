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
        private readonly ICustomerRepository _userRepository;

        public AuthenticateUser(ICustomerRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<Customer> Login(string name, string pass)
        {
            return await _userRepository.AuthenticateUserAsync(name, pass);
        }
    }
}
