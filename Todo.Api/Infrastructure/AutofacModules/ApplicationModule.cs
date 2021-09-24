using Autofac;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Infra.Repositories;
using Todo.Api.Queries;

namespace Todo.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {

        public string QueriesConnectionString { get; }
        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new TodoItemQueries(QueriesConnectionString)).As<ITodoItemQueries>().InstancePerLifetimeScope();
            builder.RegisterType<TodoItemRepository>().As<ITodoItemRepository>().InstancePerLifetimeScope();
            //builder.RegisterAssemblyTypes(typeof(CreateTodoItemCommandHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
