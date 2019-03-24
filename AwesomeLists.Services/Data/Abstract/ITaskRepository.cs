using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLists.Data.Abstract
{
    public interface ITaskRepository
    {
        void AddTask(AppTask task);

        Task<List<AppTask>> GetAllTasksByTaskListIdAsync(int id, CancellationToken token);

        Task<AppTask> FindByIdAsync(int id, CancellationToken token);

        void Update(AppTask task);

        void Delete(AppTask task);
    }
}
