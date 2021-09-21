using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Domain.Repositories
{    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string iduser);
        Task<ICollection<User>> GetUsersAsync();
        Task<User> AuthenticateUserAsync(string name, string pass);
    }
}
