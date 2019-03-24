using System.Threading;

namespace AwesomeLists.Services.User
{
    public interface IUserService
    {
        System.Threading.Tasks.Task AddAsync(Data.Entities.User user, CancellationToken token);

        System.Threading.Tasks.Task<Data.Entities.User> GetByIdAsync(string id, CancellationToken token);
    }
}
