using ToDoListApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoListApp.Domain.AggregatesModel.CustomerAggregate;

namespace ToDoListApp.Domain.AggregatesModel.TaskAggregate
{
    public class TaskItem : IAggregateRoot
    {
        public TaskItem()
        {

        }
        public TaskItem(string name, string customerid)
        {
            Name = name;
        }

        public Guid Id {get;set;}
        public string Name {get;set;}
        public bool Completed {get;set;}
        public DateTime DateCreated {get;set;}
        public Customer Customer { get; set; }
        public string CustomerID { get; set; }
    }
}