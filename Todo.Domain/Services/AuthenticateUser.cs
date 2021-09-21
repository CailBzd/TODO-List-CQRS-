using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Domain.Repositories;

namespace Todo.Domain.Services
{
    public class AuthenticateUser
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateUser(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<User> Login(string name, string pass)
        {
            return await _userRepository.AuthenticateUserAsync(name, pass);
        }
    }
}
