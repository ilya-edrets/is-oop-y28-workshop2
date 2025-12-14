namespace Core.Entities
{
    public class User
    {
        public string Name { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
