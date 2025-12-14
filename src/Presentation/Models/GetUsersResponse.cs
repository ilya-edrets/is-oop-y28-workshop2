using Core.Entities;

namespace Presentation.Models
{
    public class GetUsersResponse
    {
        public string UserName { get; set; } = string.Empty;

        public static GetUsersResponse CreateFrom(User user)
        {
            return new GetUsersResponse
            {
                UserName = user.Name
            };
        }
    }
}
