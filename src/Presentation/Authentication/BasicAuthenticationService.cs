using AspNetCore.Authentication.Basic;
using Core.Abstractions;
using System.Threading.Tasks;

namespace Presentation.Authentication
{
    internal class BasicAuthenticationService : IBasicUserValidationService
    {
        private ILoginService _loginService;

        public BasicAuthenticationService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<bool> IsValidAsync(string username, string password)
        {
            var result = await _loginService.Login(username, password);
            return result.IsSuccess;
        }
    }
}
