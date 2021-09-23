using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Domain.AggregatesModel.CustomerAggregate
{    public interface ICustomerRepository
    {
        Task<Customer> GetUserByIdAsync(string iduser);
        Task<ICollection<Customer>> GetUsersAsync();
        Task<Customer> AuthenticateUserAsync(string name, string pass);
    }
}
