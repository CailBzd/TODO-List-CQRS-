using MediatR;
using ToDoListApp.Infrastructure;
using ToDoListApp.Domain.AggregatesModel.TaskAggregate;
using ToDoListApp.Domain.AggregatesModel.CustomerAggregate;
using ToDoListApp.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace ToDoListApp.Infrastructure.Contexts
{
    public class ToDoContext : DbContext, IUnitOfWork
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers {get; set;}
        public DbSet<TaskItem> Tasks {get; set;}

        private readonly IMediator _mediator;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary K
            modelBuilder.Entity<Customer>().HasKey(u => u.Id);
            modelBuilder.Entity<TaskItem>().HasKey(ti => ti.Id);

            //Foreign K
            modelBuilder.Entity<TaskItem>().HasOne(ti => ti.Customer).WithMany(u => u.Tasks);
        }

         public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}