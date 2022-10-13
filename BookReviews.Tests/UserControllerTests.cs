using BookReviews.API.Controllers;
using BookReviews.Data;
using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using BookReviews.Tests.DataForTests;
using Moq;

namespace BookReviews.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestClass]
        public class UsersControllerTests
        {
            private Mock<IUserService> _userServiceMock;

            public UsersControllerTests()
            {
                _userServiceMock = new Mock<IUserService>();
            }

            // Creates an instance of the controller so we can test
            private UsersController GetControllerInstance()
            {
                return new UsersController(_userServiceMock.Object);
            }

            // runs every time a test starts
            [TestInitialize()]
            public void TestInitialize()
            {
                TestData.LoadData(); // Reset the test data

                // Mock functionality
                _userServiceMock.Setup(x => x.GetActiveUsersAsync()).ReturnsAsync(TestData.UserList.Where(u => u.IsDeleted == false));

                _userServiceMock.Setup(x => x.GetUserAsync(It.IsAny<int>())).ReturnsAsync((int i) => TestData.UserList.FirstOrDefault(z => z.Id == i));

                _userServiceMock.Setup(x => x.AddUserAsync(It.IsAny<UserDto>())).ReturnsAsync(
                    (UserDto target) =>
                    {
                        TestData.UserList.Add(target);
                        return target;
                    });

                _userServiceMock.Setup(x => x.DeleteUserAsync(It.IsAny<int>(), It.IsAny<int>())).Callback((int id, int currentUserId) =>
                {
                    var itemToUpdate = TestData.UserList.FirstOrDefault(u => u.Id == id);
                    if (itemToUpdate == null) return;
                    itemToUpdate.IsDeleted = true;
                    itemToUpdate.DeletedOn = DateTime.UtcNow;
                    itemToUpdate.DeletedByUserId = currentUserId;
                    return;
                });

                _userServiceMock.Setup(x => x.UndeleteUserAsync(It.IsAny<int>())).Callback((int id) =>
                {
                    var itemToUpdate = TestData.UserList.FirstOrDefault(u => u.Id == id);
                    if (itemToUpdate == null) return;
                    itemToUpdate.IsDeleted = false;
                    itemToUpdate.DeletedOn = null;
                    itemToUpdate.DeletedByUserId = null;
                    return;
                });

                _userServiceMock.Setup(x => x.IsUserDeletedAsync(It.IsAny<int>())).ReturnsAsync((int userId) =>
                {
                    var user = TestData.UserList.FirstOrDefault(u => u.Id == userId);
                    if (user != null)
                    {
                        return user.IsDeleted;
                    }
                    throw new ArgumentException($"Error.  User does not exist in the system.{userId}", nameof(userId));
                });

                _userServiceMock.Setup(x => x.AuthenticateUserAsync(It.IsAny<UserDto>())).ReturnsAsync(
                    (UserDto userDto) =>
                    {
                        var user = TestData.UserList.FirstOrDefault(e => e.Email == userDto.Email);
                        if (user == null || user.Password == null)
                        {
                            return null;
                        }
                        var decryptedPassword = DataHelpers.PasswordDecrypt(user.Password);
                        if (userDto.Password.Equals(decryptedPassword, StringComparison.Ordinal))
                        {
                            return user;
                        }
                        return null;
                    });
            }

            [TestCleanup()]
            public void TestCleanup()
            {
                // reset mocks to prepare for the next test
                _userServiceMock.Reset();
            }

            [TestMethod]
            public async Task CanGetActiveUsersAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                var expectedUsers = new List<UserDto>()
            {
                new UserDto()
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                },
                new UserDto()
                {
                    Id = 2,
                    FirstName = "Jeff",
                    LastName = "McCann",
                    Email = "jefe101073@gmail.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                }
            };

                // Act
                var users = await controller.GetActiveUsersAsync();
                var actualUsers = users.ToList();

                // Assert
                Assert.IsNotNull(actualUsers);
                Assert.AreEqual(expectedUsers.Count, actualUsers.Count);
                for (int i = 0; i < expectedUsers.Count; i++)
                {
                    Assert.AreEqual(expectedUsers[i].Id, actualUsers[i].Id);
                    Assert.AreEqual(expectedUsers[i].FirstName, actualUsers[i].FirstName);
                    Assert.AreEqual(expectedUsers[i].LastName, actualUsers[i].LastName);
                    Assert.AreEqual(expectedUsers[i].Password, actualUsers[i].Password);
                    Assert.AreEqual(expectedUsers[i].IsDeleted, actualUsers[i].IsDeleted);
                }
            }

            [TestMethod]
            public async Task CanGetSpecificUserAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                var expectedUser = new UserDto
                {
                    Id = 2,
                    FirstName = "Jeff",
                    LastName = "McCann",
                    Email = "jefe101073@gmail.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                };

                // Act
                var actualUser = await controller.GetUserAsync(2);

                // Assert
                Assert.IsNotNull(actualUser);
                Assert.AreEqual(expectedUser.Id, actualUser.Id);
                Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
                Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
                Assert.AreEqual(expectedUser.Password, actualUser.Password);
                Assert.AreEqual(expectedUser.IsDeleted, actualUser.IsDeleted);
            }

            [TestMethod]
            public async Task CanAddUserAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                var expectedUser = new UserDto
                {
                    Id = 4,
                    FirstName = "Add",
                    LastName = "McAdderson",
                    Email = "addMe@gmail.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                };

                // Act
                var actualUser = await controller.AddUserAsync(expectedUser);

                var addedUser = await controller.GetUserAsync(4);

                // Assert
                Assert.IsNotNull(addedUser);
                Assert.AreEqual(expectedUser.Id, addedUser.Id);
                Assert.AreEqual(expectedUser.FirstName, addedUser.FirstName);
                Assert.AreEqual(expectedUser.LastName, addedUser.LastName);
                Assert.AreEqual(expectedUser.Password, addedUser.Password);
                Assert.AreEqual(expectedUser.IsDeleted, addedUser.IsDeleted);
            }

            [TestMethod]
            public async Task CanDeleteUserAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                // Act
                await controller.DeleteUserAsync(2, 1);

                var deletedUser = await controller.GetUserAsync(2);

                // Assert
                Assert.IsNotNull(deletedUser);
                Assert.AreEqual(true, deletedUser.IsDeleted);
                Assert.IsNotNull(deletedUser.DeletedByUserId);
                Assert.IsNotNull(deletedUser.DeletedOn);

                Assert.IsTrue(deletedUser.DeletedOn < DateTime.UtcNow);
                Assert.IsTrue(deletedUser.DeletedOn > DateTime.UtcNow.AddDays(-1));
            }

            [TestMethod]
            public async Task CanUnDeleteUserAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                // Act
                await controller.DeleteUserAsync(2, 1);

                await controller.UndeleteUserAsync(2);

                var undeletedUser = await controller.GetUserAsync(2);

                // Assert
                Assert.IsNotNull(undeletedUser);
                Assert.AreEqual(false, undeletedUser.IsDeleted);
                Assert.IsNull(undeletedUser.DeletedByUserId);
                Assert.IsNull(undeletedUser.DeletedOn);
            }

            [TestMethod]
            public async Task CanCheckIfUserIsDeletedAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                // Act
                await controller.DeleteUserAsync(2, 1);

                var isUserDeleted = await controller.IsUserBlockedOrDeletedAsync(2);

                // Assert
                Assert.IsTrue(isUserDeleted);
            }

            [TestMethod]
            public async Task CanAuthenticateUserAsync()
            {
                // Arrange
                var controller = GetControllerInstance();

                var expectedUser = new UserDto
                {
                    Id = 4,
                    FirstName = "Add",
                    LastName = "McAdderson",
                    Email = "addMe@gmail.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                };
                await controller.AddUserAsync(expectedUser);

                // Act
                var userDto = new UserDto { Email = expectedUser.Email, Password = "password" };
                var canAuthenticate = await controller.AuthenticateUserAsync(userDto);

                // Assert
                Assert.IsNotNull(canAuthenticate);
            }
        }
    }
}
