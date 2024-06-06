using Entities;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserById(int id);
        Task<User> login(User u);
        Task<User> UpdateUser(User u, int id);
    }
}