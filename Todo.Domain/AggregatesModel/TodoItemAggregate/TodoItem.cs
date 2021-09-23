using System;
using Todo.Domain.AggregatesModel.CustomerAggregate;
using Todo.Domain.SeedWork;

namespace Todo.Domain.AggregatesModel.TodoItemAggregate
{
    public class TodoItem : Entity, IAggregateRoot
    {
        public string Title { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        public TodoItem(string title)
        {
            Title = title;
        }

        public TodoItem(string title,Guid customerId)
        {
            Title = title;
            CustomerId = customerId;
        }

    }
}