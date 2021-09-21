using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _context;

        public TodoRepository(DataContext context)
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
               .Where(TodoQueries.GetAll(userid));
        }

       
    }
}