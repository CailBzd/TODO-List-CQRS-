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

            var config = new ConfigurationBuilder()
              .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase("database");

            optionsBuilder.UseInMemoryDatabase<TodoContext>("database");//.UseSqlServer("DataSource=:memory:", sqlServerOptionsAction: o => o.MigrationsAssembly("Todo.Api"));

            return new TodoContext(optionsBuilder.Options);
        }
    }
}
