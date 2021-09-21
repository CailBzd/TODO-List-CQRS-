using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<TodoItem>().HasKey(ti => ti.Id);


            modelBuilder.Entity<TodoItem>().HasOne(ti => ti.User).WithMany(u => u.Todos);

            //modelBuilder.Entity<TodoItem>().ToTable("Todo");
            //modelBuilder.Entity<TodoItem>().Property(x => x.Id);
            //modelBuilder.Entity<TodoItem>().Property(x => x.User).HasMaxLength(120).HasColumnType("varchar(120)");
            //modelBuilder.Entity<TodoItem>().Property(x => x.Title).HasMaxLength(160).HasColumnType("varchar(160)");
            //modelBuilder.Entity<TodoItem>().Property(x => x.Date);
            //modelBuilder.Entity<TodoItem>().HasIndex(b => b.User);
        }
    }
}