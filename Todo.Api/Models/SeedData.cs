using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.CustomerAggregate;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Infra.Contexts;

namespace Todo.Api.Models
{
    public class SeedData
    {
        public async Task SeedDatabaseAsync(TodoContext context)
        {
            using (context)
            {
                var user = new Customer
                {
                    Id = new Guid(),
                    Username = "Pierre",
                    Password = "0123456789",
                    Todos = new List<TodoItem>()
                    {
                        new TodoItem("Todo Pierre 1"),new TodoItem("Todo Pierre 2"),new TodoItem("Todo Pierre 3")
                    }
                };

                context.Customers.Add(user);

                var user2 = new Customer
                {
                    Id = new Guid(),
                    Username = "Patrick",
                    Password = "0123456789",
                    Todos = new List<TodoItem>()
                    {
                        new TodoItem("Todo Patrick 1"),new TodoItem("Todo Patrick 2"),new TodoItem("Todo Patrick 3")
                    }
                };

                context.Customers.Add(user2);

                await context.SaveChangesAsync();
            }
                
        }
    }
}
