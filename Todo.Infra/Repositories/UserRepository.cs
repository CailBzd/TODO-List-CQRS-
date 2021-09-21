using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entities;
using Todo.Infra.Contexts;
using Todo.Domain.Repositories;

namespace Todo.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            this._context = context;
        }

        //GetUserById
        public async Task<User> GetUserByIdAsync(string iduser)
        {
            var user = await _context.Users.Where(u => u.Id.ToString() == iduser.ToString()).FirstOrDefaultAsync();
            if (user == null)
            { throw new Exception("User not found"); }
            return user;

        }

        //GetUsers
        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //AuthUser
        public async Task<User> AuthenticateUserAsync(string name, string pass)
        {
            var user = await _context.Users.Where(user => user.Username == name && user.Password == pass).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Authentification refused");
            }
            return user;
        }
    }
}
