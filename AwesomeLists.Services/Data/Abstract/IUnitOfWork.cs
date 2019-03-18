using System.Threading.Tasks;

namespace AwesomeLists.Data.Abstract
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
