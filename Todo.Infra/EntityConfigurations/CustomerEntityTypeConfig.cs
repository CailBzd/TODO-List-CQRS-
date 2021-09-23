using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.AggregatesModel.CustomerAggregate;
using Todo.Domain.AggregatesModel.TodoItemAggregate;
using Todo.Infra.Contexts;

namespace Todo.Infra.EntityConfigurations
{
    class CustomerEntityTypeConfig : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> todoConfig)
        {
            todoConfig.ToTable("customers", TodoContext.DEFAULT_SCHEMA);

            todoConfig.HasKey(o => o.Id);


        }
    }
}
