using Core.Entities;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IUserStorage
    {
        Task AddUser(User user);

        Task<User?> FindUser(string name);
    }
}
