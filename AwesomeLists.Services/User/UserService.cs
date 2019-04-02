using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AwesomeLists.Data.Abstract;

namespace AwesomeLists.Services.User
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async System.Threading.Tasks.Task AddAsync(Data.Entities.User user, CancellationToken token)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.Id)
                || string.IsNullOrWhiteSpace(user.FirstName)
                || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("Fields are empty", nameof(user));
            }

            _userRepository.Add(user);

            token.ThrowIfCancellationRequested();
            await _unitOfWork.SaveAsync(token);
        }

        public async System.Threading.Tasks.Task DeleteAsync(Data.Entities.User user, CancellationToken token)
        {
            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync(token);
        }

        public async Task<IReadOnlyCollection<Data.Entities.User>> GetAllAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return await _userRepository.GetAllAsync(token);
        }

        public async Task<Data.Entities.User> GetByIdAsync(string id, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            token.ThrowIfCancellationRequested();
            return await _userRepository.GetByIdAsync(id, token);
        }
    }
}
