using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLIsts.Data
{
    public sealed class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void AddTask(AppTask task)
        {
            _dbContext.Add(task);
        }

        public async Task<List<AppTask>> GetAllTasksByTaskListIdAsync(int id, CancellationToken token)
        {
            return await _dbContext.Tasks.Where(task => task.TaskListId == id).ToListAsync(token);
        }

        public async Task<AppTask> FindByIdAsync(int id, CancellationToken token)
        {
            return await _dbContext.Tasks.Where(task => task.Id == id).FirstOrDefaultAsync(token);
        }

        public void Update(AppTask task)
        {
            _dbContext.Tasks.Update(task);
        }

        public void Delete(AppTask task)
        {
            _dbContext.Entry(task).State = EntityState.Deleted;
        }
    }
}
