using ToDoListApp.Domain.SeedWork;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ToDoListApp.Domain.AggregatesModel.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(int iduser);
        Task<ICollection<Customer>> GetCustomersAsync();
        Task<Customer> AuthenticateCustomerAsync(string name, string pass);
    }
}