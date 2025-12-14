using Core.Abstractions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Core.Services
{
    internal class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ILogger<LoginService> _logger;

        public LoginService(IUserService userService, IPasswordHashService passwordHashService, ILogger<LoginService> logger)
        {
            _userService = userService;
            _passwordHashService = passwordHashService;
            _logger = logger;
        }

        public async Task<OperationResult> Login(string name, string password)
        {
            var result = await _userService.GetUser(name);
            if (!result.IsSuccess || result.Result == null)
            {
                return result;
            }

            var user = result.Result;
            if (!_passwordHashService.VerifyPassword(password, user.PasswordHash))
            {
                _logger.LogInformation("Log in attempt was failed for {name}", name);
                return OperationResult.Fail("wrong password");
            }

            _logger.LogInformation("Log in was successful for {name}", name);
            return OperationResult.Success();
        }
    }
}
