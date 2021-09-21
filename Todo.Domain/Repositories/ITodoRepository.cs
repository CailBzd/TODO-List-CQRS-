using System;
using System.Collections.Generic;
using Todo.Domain.Entities;

namespace Todo.Domain.Repositories
{
    public interface ITodoRepository
    {
        void Create(TodoItem todo);
        IEnumerable<TodoItem> GetAll(string userid);
    }
}