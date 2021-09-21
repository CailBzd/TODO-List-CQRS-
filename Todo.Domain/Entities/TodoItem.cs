using System;

namespace Todo.Domain.Entities
{
    public class TodoItem : Entity
    {
        public TodoItem()
        {
        }
        public TodoItem(string title)
        {
            Title = title;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public User User{ get; set; }
      
    }
}