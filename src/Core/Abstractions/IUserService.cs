using Core.Entities;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IUserService
    {
        Task<OperationResult> CreateUser(string name, string password);

        Task<OperationResult<User>> GetUser(string name);
    }
}
