using AwesomeLists.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLists.Data.Abstract
{
    public interface IUserRepository
    {
        void Add(User user);

        Task<User> GetByIdAsync(string id, CancellationToken token);

        void Delete(User user);

        Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken token);
    }
}
