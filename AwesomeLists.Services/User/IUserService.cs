using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLists.Services.User
{
    public interface IUserService
    {
        System.Threading.Tasks.Task AddAsync(Data.Entities.User user, CancellationToken token);

        Task<Data.Entities.User> GetByIdAsync(string id, CancellationToken token);

        Task<IReadOnlyCollection<Data.Entities.User>> GetAllAsync(CancellationToken token);

        System.Threading.Tasks.Task DeleteAsync(Data.Entities.User user, CancellationToken token);
    }
}
