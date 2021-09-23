using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Todo.Infra.Contexts;

namespace Todo.Api.Infrastructure.Factories
{
    public class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoContext>
    {

        public TodoContext CreateDbContext(string[] args)
        {
               var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();

            optionsBuilder.UseInMemoryDatabase("Database");

            return new TodoContext(optionsBuilder.Options);
        }
    }
}
