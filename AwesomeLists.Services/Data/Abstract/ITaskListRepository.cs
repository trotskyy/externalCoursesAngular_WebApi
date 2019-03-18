using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeLists.Data.Abstract
{
    public interface ITaskListRepository
    {
        Task<TaskList> GetByIdAsync(int id);

        void Add(TaskList taskList);

        void Delete(TaskList taskList);

        Task<List<TaskListSummary>> GetTaskListsSummaryAsync(string userId);
    }
}
