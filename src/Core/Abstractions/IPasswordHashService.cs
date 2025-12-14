namespace Core.Abstractions
{
    public interface IPasswordHashService
    {
        string GetHash(string password);

        bool VerifyPassword(string password, string hash);
    }
}
