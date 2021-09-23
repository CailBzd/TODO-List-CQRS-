using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Infra.Contexts;

namespace Todo.Api.Queries
{
    public class TodoItemQueries : ITodoItemsQueries
    {

        private string _connectionString = string.Empty;
        public TodoItemQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<ICollection<TodoItemSummary>> GetTodoItemsFromCustomerAsync(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(@"Select * from Todos Where CustomerId = @userId", new { userId });

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapTodoItems(result);
            }
        }

        public async Task<ICollection<TodoItemSummary>> GetTodoItemsAsync()
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(@"Select * from Todos");

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapTodoItems(result);
            }

        }

        private ICollection<TodoItemSummary> MapTodoItems(dynamic result)
        {
            List<TodoItemSummary> todos = new List<TodoItemSummary>();
            foreach (dynamic item in result)
            {
                var todoItem = new TodoItemSummary()
                {
                    title = item.t
                };

                todos.Add(todoItem);
            }

            ICollection<TodoItemSummary> col = todos;

            return col;
        }
    }
}