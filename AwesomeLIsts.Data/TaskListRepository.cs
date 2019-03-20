using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeLIsts.Data
{
    public sealed class TaskListRepository : ITaskListRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskListRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(TaskList taskList)
        {
            _dbContext.Add(taskList);
        }

        public void Delete(TaskList taskList)
        {
            _dbContext.Entry(taskList).State = EntityState.Deleted;
        }

        public async Task<TaskList> GetByIdAsync(int id)
        {
            return await _dbContext.TaskLists
                .Include(taskList => taskList.Tasks)
                .Where(taskList => taskList.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TaskListSummary>> GetTaskListsSummaryAsync(string userId)
        {
            return await _dbContext.TaskLists
                .AsNoTracking()
                .Include(taskList => taskList.Tasks)
                .Where(taskList => taskList.UserId == userId)
                .Select(taskList => new TaskListSummary
                {
                    TaskListId = taskList.Id,
                    ListName = taskList.Name,
                    ToDoCount = taskList.Tasks.Count(task => task.Status == Status.ToDo),
                    InProgressCount = taskList.Tasks.Count(task => task.Status == Status.InProgress),
                    DoneCount = taskList.Tasks.Count(task => task.Status == Status.Done),
                })
                .ToListAsync();
        }
    }
}
