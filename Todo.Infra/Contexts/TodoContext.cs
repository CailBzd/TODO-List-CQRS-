using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.CustomerAggregate;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Domain.SeedWork;
using Todo.Infra.EntityConfigurations;

namespace Todo.Infra.Contexts
{
    public class TodoContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "todolist";

        public DbSet<Customer> Customers { get; set; }
        public DbSet<TodoItem> Todos { get; set; }

        private readonly IMediator _mediator;

        public TodoContext(DbContextOptions<TodoContext> options): base(options){
            Console.WriteLine("TodoContext1");
        }

        public TodoContext(DbContextOptions<TodoContext> options, IMediator mediator) : base(options)
        {
            Console.WriteLine("TodoContext2");
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoEntityTypeConfig());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfig());

        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class TodoContextDesignFactor : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase("Database");

            return new TodoContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                throw new NotImplementedException();
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}