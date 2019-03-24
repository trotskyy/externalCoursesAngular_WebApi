using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLists.Services.Task
{
    public interface ITaskService
    {
        Task<AppTask> AddTaskAsync(AppTask task, CancellationToken token);

        System.Threading.Tasks.Task UpdateTaskAsync(int id, AppTask task, CancellationToken token);

        Task<List<AppTask>> GetByTaskListIdAsync(int id, CancellationToken token);

        Task<AppTask> GetByIdAsync(int id, CancellationToken token);

        System.Threading.Tasks.Task DeleteAsync(int id, CancellationToken token);
    }
}
