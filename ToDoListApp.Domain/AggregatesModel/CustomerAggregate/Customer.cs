using ToDoListApp.Domain.SeedWork;
using ToDoListApp.Domain.AggregatesModel.TaskAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoListApp.Domain.AggregatesModel.CustomerAggregate
{
    public class Customer : IAggregateRoot
    {
        public Customer()
        {
            Tasks = new List<TaskItem>();
        }

        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }
    }
}