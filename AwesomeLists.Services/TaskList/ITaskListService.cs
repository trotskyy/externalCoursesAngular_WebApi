using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwesomeLists.Services.TaskList
{
    public interface ITaskListService
    {
        Task<Data.Entities.TaskList> GetByIdAsync(int id);

        Task<Data.Entities.TaskList> AddAsync(Data.Entities.TaskList taskList);

        System.Threading.Tasks.Task DeleteAsync(Data.Entities.TaskList taskList);

        Task<List<TaskListSummary>> GetTaskListsSummaryAsync(string userId);

        System.Threading.Tasks.Task UpdateNameAsync(int id, string name);
    }
}
