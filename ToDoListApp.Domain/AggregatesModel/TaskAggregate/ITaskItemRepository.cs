using ToDoListApp.Domain.SeedWork;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ToDoListApp.Domain.AggregatesModel.TaskAggregate
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
        Task<TaskItem> Add(TaskItem task);

        Task<TaskItem> GetAsync(string taskId);

        Task<ICollection<TaskItem>> GetTasksByUser(string iduser);
    }
}