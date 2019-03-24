using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLists.Data.Abstract
{
    public interface ITaskListRepository
    {
        Task<TaskList> GetByIdAsync(int id, CancellationToken token);

        void Add(TaskList taskList);

        void Delete(TaskList taskList);

        Task<List<TaskListSummary>> GetTaskListsSummaryAsync(string userId, CancellationToken token);
    }
}
