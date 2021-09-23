using System;
using System.Collections.Generic;
using Todo.Domain.AggregatesModel.TodoItemAggregate;

namespace Todo.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer
    {
        public Customer()
        {
            Todos = new List<TodoItem>();
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<TodoItem> Todos { get; set; }

    }
}
