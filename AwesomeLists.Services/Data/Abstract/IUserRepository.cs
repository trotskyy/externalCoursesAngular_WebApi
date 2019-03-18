using AwesomeLists.Data.Entities;
using System.Threading.Tasks;

namespace AwesomeLists.Data.Abstract
{
    public interface IUserRepository
    {
        void Add(User user);

        Task<User> GetByIdAsync(string id);
    }
}
