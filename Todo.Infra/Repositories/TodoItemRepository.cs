using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Domain.SeedWork;
using Todo.Infra.Contexts;

namespace Todo.Infra.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TodoItemRepository(TodoContext context)
        {
            _context = context;
        }

        public void Create(TodoItem todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAll(string userid)
        {
            return _context.Todos
               .AsNoTracking()
               .Where(u => u.CustomerId.ToString() == userid);
        }

       
    }
}