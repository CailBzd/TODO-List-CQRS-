using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Api.Queries
{
    public record TodoItemSummary
    {
        public string title { get; set; }

    }
}
