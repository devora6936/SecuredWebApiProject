using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        int CheckPass(string password);
        Task<User> GetUserById(int id);
        Task<User> login(User u);
        Task<User> UpdateUser(User u, int id);
    }
}