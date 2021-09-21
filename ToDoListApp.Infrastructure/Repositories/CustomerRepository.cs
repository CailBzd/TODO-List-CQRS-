using ToDoListApp.Domain.AggregatesModel.CustomerAggregate;
using ToDoListApp.Domain.AggregatesModel.TaskAggregate;
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Infrastructure.Contexts;
using System.Collections.Generic;
using ToDoListApp.Domain.SeedWork;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ToDoListApp.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ToDoContext _context;

                public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public CustomerRepository(ToDoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));            
        }

       //GetCustomerById
        public async Task<Customer> GetCustomerByIdAsync(int idCustomer)
        {
            var Customer = await _context.Customers.Where(u => u.Id.ToString() == idCustomer.ToString()).FirstOrDefaultAsync();
            if (Customer == null)
            { throw new Exception("Customer not found"); }
            return Customer;

        }

        //GetCustomers
        public async Task<ICollection<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        //AuthCustomer
        public async Task<Customer> AuthenticateCustomerAsync(string name,string pass)
        {
            var Customer = await _context.Customers.Where(Customer => Customer.CustomerName == name && Customer.Password == pass).FirstOrDefaultAsync();
            if (Customer == null)
            {
                throw new Exception("Authentification refused");
            }
            return Customer;
        }
    }
}