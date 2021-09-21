using System;
using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Domain.Queries
{
    public static class TodoQueries
    {
        public static Expression<Func<TodoItem, bool>> GetAll(string userId)
        {
            return x => x.User.Id.ToString() == userId;
        }
    }
}