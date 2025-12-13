using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface ILoginService
    {
        public Task<OperationResult> Login(string username, string password);
    }
}
