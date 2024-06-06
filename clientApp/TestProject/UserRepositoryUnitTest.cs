using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositories;

namespace TestProject
{
    public class UserRepositoryUnitTest
    {
        [Fact]
        public async void GetUser_ValidCredentils_ReturnsUser()
        {
            var user = new User { Email = "test@example.com", Password = "password" };
            var mockContext = new Mock<MarketContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);

            var userRepository = new UserRepository(mockContext.Object);

            var result = await userRepository.login(user);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task AddUser_ValidUser_ReturnsUser()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };
            var mockContext = new Mock<MarketContext>();
            var mockSet = new Mock<DbSet<User>>();

            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            mockContext.Setup(m => m.Users.AddAsync(user, It.IsAny<CancellationToken>()))
                       .Returns(new ValueTask<EntityEntry<User>>(Task.FromResult((EntityEntry<User>)null)));
            mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(1);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.AddUser(user);

            // Assert
            Assert.Equal(user, result);
            mockSet.Verify(m => m.AddAsync(user, It.IsAny<CancellationToken>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }


        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            // Arrange
            var user = new User { FirstName = "dvori", LastName = "rottman", Email = "dvori@gmail.com", Password = "password" };
            var updatedUser = new User { FirstName = "updated", LastName = "user", Email = "updated@gmail.com", Password = "newpassword" };

            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<MarketContext>();

            mockSet.Setup(m => m.Update(It.IsAny<User>()));
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var userRepository = new UserRepository(mockContext.Object);

            // Act
            var result = await userRepository.UpdateUser( updatedUser,user.UserId);

            // Assert
            Assert.Equal("updated@gmail.com", result.Email);
        }

    }
}