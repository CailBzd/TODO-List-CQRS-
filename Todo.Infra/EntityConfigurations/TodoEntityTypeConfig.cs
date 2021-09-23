using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Infra.Contexts;

namespace Todo.Infra.EntityConfigurations
{
    class TodoEntityTypeConfig : IEntityTypeConfiguration<TodoItem>
    {

        public void Configure(EntityTypeBuilder<TodoItem> todoConfig)
        {
            todoConfig.ToTable("todos", TodoContext.DEFAULT_SCHEMA);

            todoConfig.HasKey(o => o.Id);

            todoConfig.HasOne(o => o.Customer).WithMany(u => u.Todos);

        }
    }
}
