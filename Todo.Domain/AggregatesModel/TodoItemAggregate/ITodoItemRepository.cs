using Todo.Domain.SeedWork;
using System.Collections.Generic;

namespace Todo.Domain.AggregatesModel.TodoItemAggregate

{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        void Create(TodoItem todo);
        IEnumerable<TodoItem> GetAll(string userid);
    }
}