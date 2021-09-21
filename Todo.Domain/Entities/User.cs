using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Entities
{
    public class User
    {
        public User()
        {
            Todos = new List<TodoItem>();
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<TodoItem> Todos { get; set; }

    }
}
