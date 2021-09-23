using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.CustomerAggregate;
using Todo.Infra.Contexts;

namespace Todo.Api.Models
{
    public class SeedData
    {
        public async Task SeedDatabaseAsync(TodoContext context)
        {
            Console.WriteLine("-->  SeedData : Migrate");

            using (context)
            {
                Console.WriteLine("-->  SeedData : Customer");

                var user = new Customer
                {
                    Id = new Guid(),
                    Username = "Pierre",
                    Password = "0123456789"
                };

                Console.WriteLine("-->  SeedData : AddRange");

                //context.Customers.AddRange(
                //    new Customer { Id = new Guid(), Username = "Pierre", Password = "0123456789" },
                //    new Customer { Id = new Guid(), Username = "Patrick", Password = "0123456789" }
                //    );

                context.Customers.Add(user);

                Console.WriteLine("-->  SeedData : SaveChanges");

                await context.SaveChangesAsync();
            }
                
        }
    }
}
