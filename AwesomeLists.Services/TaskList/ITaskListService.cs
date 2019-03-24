using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AwesomeLists.Services.TaskList
{
    public interface ITaskListService
    {
        Task<Data.Entities.TaskList> GetByIdAsync(int id, CancellationToken token);

        Task<Data.Entities.TaskList> AddAsync(Data.Entities.TaskList taskList, CancellationToken token);

        System.Threading.Tasks.Task DeleteAsync(Data.Entities.TaskList taskList, CancellationToken token);

        Task<List<TaskListSummary>> GetTaskListsSummaryAsync(string userId, CancellationToken token);

        System.Threading.Tasks.Task UpdateNameAsync(int id, string name, CancellationToken token);
    }
}
