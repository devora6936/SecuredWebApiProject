using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private MarketContext _marketContext;
        public UserRepository(MarketContext marketContext)
        {
            _marketContext = marketContext;
        }

        public async Task<User> login(User u)
        {
            return  await _marketContext.Users.Where(user => user.Email == u.Email && user.Password == u.Password).FirstOrDefaultAsync();
        }
        public  async Task<User> GetUserById(int id)
        {
            return await _marketContext.Users.FindAsync(id);
        }

        public async Task<User> AddUser(User user)
        {
            await _marketContext.Users.AddAsync(user);
            await _marketContext.SaveChangesAsync();
            return user;
        }


        public async Task<User> UpdateUser( User u,int id)
        {
            u.UserId = id;
            _marketContext.Users.Update(u);
            await _marketContext.SaveChangesAsync();
            return u;
        }

    }
}
