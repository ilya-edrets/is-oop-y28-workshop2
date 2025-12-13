using Core.Abstractions;
using Core.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Core.Services
{
    internal class UserService : IUserService
    {
        private IUserStorage _storage;
        private IPasswordHashService _passwordHashService;
        private ILogger<UserService> _logger;

        public UserService(IUserStorage storage, IPasswordHashService passwordHashService, ILogger<UserService> logger)
        {
            _storage = storage;
            _passwordHashService = passwordHashService;
            _logger = logger;
        }

        public async Task<OperationResult> CreateUser(string userName, string password)
        {
            var user = new User
            {
                Name = userName,
                PasswordHash = _passwordHashService.GetHash(password),
            };

            await _storage.AddUser(user);

            _logger.LogInformation("New user {userName} was created", userName);

            return OperationResult.Success();
        }

        public async Task<OperationResult<User>> GetUser(string userName)
        {
            var user = await _storage.FindUser(userName);
            if (user != null)
            {
                return OperationResult<User>.Success(user);
            }

            return OperationResult<User>.Fail("User not found");
        }
    }
}
