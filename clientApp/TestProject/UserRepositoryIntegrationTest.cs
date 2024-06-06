using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserRepositoryIntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly MarketContext _dbContext;
        private readonly UserRepository _userRepository;

        public UserRepositoryIntegrationTest(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _userRepository = new UserRepository(_dbContext);
        }

        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
            var email = "test@example.com";
            var password = "password";
            var user = new User { Email = email, Password = password, FirstName = "test", LastName = "name" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var result = await _userRepository.login(user);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddUser_CreatesUser()
        {

            var newUser = new User
            {
                Email = "testuser@example.com",
                Password = "password",
                FirstName = "Test",
                LastName = "User"
            };


            var resultUser = await _userRepository.AddUser(newUser);


            Assert.NotNull(resultUser);
            Assert.Equal("testuser@example.com", resultUser.Email);
            Assert.Equal("password", resultUser.Password);
            Assert.Equal("Test", resultUser.FirstName);
            Assert.Equal("User", resultUser.LastName);

            _dbContext.Users.Remove(resultUser);
            await _dbContext.SaveChangesAsync();
        }


        [Fact]
        public async Task UpdateUser_ValidId_UpdatesUser()
        {
            // Arrange
            var originalUser = new User { Email = "test@example.com", Password = "Password", FirstName = "Original", LastName = "User" };
            await _dbContext.Users.AddAsync(originalUser);
            await _dbContext.SaveChangesAsync();
            var userId = originalUser.UserId;

            // Detach the original user to simulate detached state
            _dbContext.Entry(originalUser).State = EntityState.Detached;

            var updatedUser = new User { UserId = userId, Email = "updated@example.com", Password = "NewPass", FirstName = "Updated", LastName = "User" };

            // Act
            var result = await _userRepository.UpdateUser( updatedUser,userId);

            // Reload the user from the database to confirm changes
            var reloadedUser = await _dbContext.Users.FindAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);  // Ensure the user's ID remains the same
            Assert.Equal(updatedUser.Email, result.Email);
            Assert.Equal(updatedUser.Password, result.Password);
            Assert.Equal(updatedUser.FirstName, result.FirstName);
            Assert.Equal(updatedUser.LastName, result.LastName);

            // Confirm changes in the database
            Assert.Equal(updatedUser.Email, reloadedUser.Email);
            Assert.Equal(updatedUser.Password, reloadedUser.Password);
            Assert.Equal(updatedUser.FirstName, reloadedUser.FirstName);
            Assert.Equal(updatedUser.LastName, reloadedUser.LastName);

            // Clean up
            _dbContext.Users.Remove(reloadedUser);
            await _dbContext.SaveChangesAsync();
        }

    }
}

