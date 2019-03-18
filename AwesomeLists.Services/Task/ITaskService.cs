using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeLists.Services.Task
{
    public interface ITaskService
    {
        Task<AppTask> AddTaskAsync(AppTask task);

        System.Threading.Tasks.Task UpdateTaskAsync(int id, AppTask task);

        Task<List<AppTask>> GetByTaskListIdAsync(int id);

        Task<AppTask> GetByIdAsync(int id);

        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}
