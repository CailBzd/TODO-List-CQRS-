using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Infra.Contexts;

namespace Todo.Api.Queries
{
    public class TodoItemQueries : ITodoItemQueries
    {

        private string _connectionString = string.Empty;
        public TodoItemQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<ICollection<TodoItemSummary>> GetTodoItemsFromCustomerAsync(Guid userId)
        {
            var connectionStringBuilder =new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

                DbContextOptions<TodoContext> options;
                var builder = new DbContextOptionsBuilder<TodoContext>();
                builder.UseInMemoryDatabase("database");
                options = builder.Options;

            using (var context = new TodoContext(options))
            {
                List<TodoItem> result = await context.Todos.Where(t => t.CustomerId == userId).ToListAsync();

                return MapTodoItems(result);
            }

            throw new NullReferenceException();
        }

        public async Task<ICollection<TodoItemSummary>> GetTodoItemsAsync()
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(@"Select * from Todos");

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return null;//MapTodoItems(result);
            }

        }

        private ICollection<TodoItemSummary> MapTodoItems(List<TodoItem> result)
        {
            List<TodoItemSummary> todos = new List<TodoItemSummary>();
            foreach (TodoItem item in result)
            {
                var todoItem = new TodoItemSummary()
                {
                    title = item.Title
                };

                todos.Add(todoItem);
            }

            ICollection<TodoItemSummary> col = todos;

            return col;
        }
    }
}