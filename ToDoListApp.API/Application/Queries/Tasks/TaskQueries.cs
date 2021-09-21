
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Domain.AggregatesModel.TaskAggregate;

namespace ToDoListApp.API.Application.Queries.Tasks
{
    public class TaskQueries : ITaskQueries
    {
        private readonly ITaskItemRepository _taskRepository;
        public TaskQueries(ITaskItemRepository taskrepo)
        {
            _taskRepository = taskrepo;
        }

        public async Task<ICollection<TaskItem>> GetTasksFromCustomerAsync(string customerId)
        {
            var result = await _taskRepository.GetTasksByUser(customerId);

            return MapTaskItems(result);
        }
        private ICollection<TaskItem> MapTaskItems(dynamic result)
        {
            ICollection<TaskItem> taskItems = null;

            foreach (dynamic item in result)
            {
                var taskItem = new TaskItem
                {
                    name = item[0].name,
                    customerid = item[0].customerId
                };

                taskItems.Add(taskItem);
            }

            return taskItems;
        }
    }

}