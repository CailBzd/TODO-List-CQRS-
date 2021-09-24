using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Infra.Contexts;
using Todo.Domain.AggregatesModel.CustomerAggregate;

namespace Todo.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TodoContext _context;

        public CustomerRepository(TodoContext context)
        {
            this._context = context;
        }

        //GetUserById
        public async Task<Customer> GetUserByIdAsync(string iduser)
        {
            var user = await _context.Customers.Where(u => u.Id.ToString() == iduser.ToString()).FirstOrDefaultAsync();
            if (user == null)
            { throw new Exception("User not found"); }
            return user;

        }

        //GetUsers
        public async Task<ICollection<Customer>> GetUsersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        //AuthUser
        public async Task<Customer> AuthenticateUserAsync(string name, string pass)
        {

            var user = await _context.Customers.Where(user => user.Username == name && user.Password == pass).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Authentification refused");
            }
            return user;
        }
    }
}
