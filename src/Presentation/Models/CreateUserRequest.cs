namespace Presentation.Models
{
    public class CreateUserRequest
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
