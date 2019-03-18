using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;
using System;
using System.Threading.Tasks;

namespace AwesomeLIsts.Data
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(User user)
        {
            _dbContext.Add(user);
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }
    }
}
