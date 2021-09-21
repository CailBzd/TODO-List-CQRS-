using ToDoListApp.Domain.SeedWork;

namespace ToDoListApp.Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
