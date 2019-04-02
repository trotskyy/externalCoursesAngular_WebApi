using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public void Delete(User user)
        {
            _dbContext.Entry(user).State = EntityState.Deleted;
        }

        public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return await _dbContext.Users.ToArrayAsync(token);
        }

        public async Task<User> GetByIdAsync(string id, CancellationToken token)
        {
            return await _dbContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync(token);
        }
    }
}
