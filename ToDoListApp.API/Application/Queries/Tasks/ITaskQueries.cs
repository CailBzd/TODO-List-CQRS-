using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListApp.API.Application.Queries.Tasks
{
    public interface ITaskQueries
    {
        Task<ICollection<TaskItem>> GetTasksFromCustomerAsync(string customerId);
    }
}