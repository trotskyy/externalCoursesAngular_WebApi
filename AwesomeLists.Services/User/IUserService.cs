namespace AwesomeLists.Services.User
{
    public interface IUserService
    {
        System.Threading.Tasks.Task AddAsync(Data.Entities.User user);

        System.Threading.Tasks.Task<Data.Entities.User> GetByIdAsync(string id);
    }
}
