using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.TodoItemAggregate;

namespace Todo.Api.Queries
{
    public interface ITodoItemQueries
    {
        Task<ICollection<TodoItemSummary>> GetTodoItemsAsync();
        Task<ICollection<TodoItemSummary>> GetTodoItemsFromCustomerAsync(Guid userId);
    }
}
