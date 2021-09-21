using ToDoListApp.Domain.AggregatesModel.TaskAggregate;
using ToDoListApp.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToDoListApp.Domain.SeedWork;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ToDoListApp.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ToDoContext _context;

       public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public TaskItemRepository(ToDoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TaskItem> Add(TaskItem task)
        {
             _context.Tasks.Add(task);
             await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem> GetAsync(string taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id.ToString() == taskId.ToString());
        
            return task;
        }

       public async Task<ICollection<TaskItem>> GetTasksByUser(string iduser)
        {
            return await _context.Tasks.Where(task => task.CustomerID.ToString() == iduser.ToString()).ToListAsync();
        }
    }
}