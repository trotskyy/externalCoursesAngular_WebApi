using AwesomeLists.Data.Abstract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwesomeLIsts.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> SaveAsync(CancellationToken token)
        {
            return await _dbContext.SaveChangesAsync(token);
        }
    }
}
