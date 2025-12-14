using Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // AllowAnonymous позволяет делать вызовы метода незалогиненым пользователям не смотря на наличие атрибута Authorize
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
             await _userService.CreateUser(request.UserName, request.Password);
            return base.Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsers();

            if (!users.IsSuccess || users.Result is null)
            {
                return NotFound();
            }

            return base.Ok(users.Result.Select(GetUsersResponse.CreateFrom));
        }
    }
}
