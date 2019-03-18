using System;
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

        public async System.Threading.Tasks.Task AddAsync(Data.Entities.User user)
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
            await _unitOfWork.SaveAsync();
        }

        public async Task<Data.Entities.User> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
    }
}
