
namespace BookReviews.Tests
{
    [TestClass]
    public class AuthorControllerTests
    {
        private Mock<IAuthorService> _authorServiceMock;

        public AuthorControllerTests()
        {
            _authorServiceMock = new Mock<IAuthorService>();
        }

        // Creates an instance of the controller so we can test
        private AuthorsController GetControllerInstance()
        {
            return new AuthorsController(_authorServiceMock.Object);
        }
        // runs every time a test starts
        [TestInitialize()]
        public void TestInitialize()
        {
            TestData.LoadData(); // Reset the test data

            // Mock functionality
            _authorServiceMock.Setup(x => x.GetActiveAuthorsAsync()).ReturnsAsync(TestData.AuthorList.Where(u => u.IsDeleted == false));

            _authorServiceMock.Setup(x => x.GetAuthorByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => TestData.AuthorList.FirstOrDefault(z => z.Id == i));

            _authorServiceMock.Setup(x => x.AddAuthorAsync(It.IsAny<AuthorDto>())).ReturnsAsync(
                (AuthorDto target) =>
                {
                    TestData.AuthorList.Add(target);
                    return target;
                });

            _authorServiceMock.Setup(x => x.DeleteAuthorAsync(It.IsAny<AuthorDto>())).Callback((AuthorDto authorDto) =>
            {
                var itemToUpdate = TestData.AuthorList.FirstOrDefault(u => u.Id == authorDto.Id);
                if (itemToUpdate == null) return;
                itemToUpdate.IsDeleted = true;
                itemToUpdate.DeletedOn = DateTime.UtcNow;
                itemToUpdate.DeletedByUserId = authorDto.DeletedByUserId;
                return;
            });
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            // reset mocks to prepare for the next test
            _authorServiceMock.Reset();
        }

        [TestMethod]
        public async Task CanGetActiveAuthorsAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedAuthors = new List<AuthorDto>()
            {
                new AuthorDto
                {
                    Id = 1,
                    FirstName = "Stephen",
                    LastName = "King",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 2,
                    FirstName = "Frank",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 3,
                    FirstName = "Brian",
                    LastName = "Herbert",
                    IsDeleted = false
                }
            };

            // Act
            var authors = await controller.GetActiveAuthorsAsync();
            var actualAuthors = authors.ToList();

            // Assert
            Assert.IsNotNull(actualAuthors);
            Assert.AreEqual(expectedAuthors.Count, actualAuthors.Count);
            for (int i = 0; i < expectedAuthors.Count; i++)
            {
                Assert.AreEqual(expectedAuthors[i].Id, actualAuthors[i].Id);
                Assert.AreEqual(expectedAuthors[i].FirstName, actualAuthors[i].FirstName);
                Assert.AreEqual(expectedAuthors[i].LastName, actualAuthors[i].LastName);
                Assert.AreEqual(expectedAuthors[i].IsDeleted, actualAuthors[i].IsDeleted);
            }
        }

        [TestMethod]
        public async Task CanGetAuthorByIdAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedAuthors = new List<AuthorDto>()
            {
                new AuthorDto
                {
                    Id = 1,
                    FirstName = "Stephen",
                    LastName = "King",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 2,
                    FirstName = "Frank",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 3,
                    FirstName = "Brian",
                    LastName = "Herbert",
                    IsDeleted = false
                }
            };

            // Act
            var actualAuthor = await controller.GetAuthorByIdAsync(2);

            // Assert
            Assert.IsNotNull(actualAuthor);

            Assert.AreEqual(expectedAuthors[1].Id, actualAuthor.Id);
            Assert.AreEqual(expectedAuthors[1].FirstName, actualAuthor.FirstName);
            Assert.AreEqual(expectedAuthors[1].LastName, actualAuthor.LastName);
            Assert.AreEqual(expectedAuthors[1].IsDeleted, actualAuthor.IsDeleted);
        }

        [TestMethod]
        public async Task CanAddAuthorAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedAuthors = new List<AuthorDto>()
            {
                new AuthorDto
                {
                    Id = 1,
                    FirstName = "Stephen",
                    LastName = "King",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 2,
                    FirstName = "Frank",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 3,
                    FirstName = "Brian",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 4,
                    FirstName = "Anne",
                    LastName = "Rice",
                    IsDeleted = false
                }
            };

            // Act
            var addAuthor = await controller.AddAuthorAsync(expectedAuthors[3]);
            var actualAuthor = await controller.GetAuthorByIdAsync(4);

            // Assert
            Assert.IsNotNull(actualAuthor);

            Assert.AreEqual(expectedAuthors[3].Id, actualAuthor.Id);
            Assert.AreEqual(expectedAuthors[3].FirstName, actualAuthor.FirstName);
            Assert.AreEqual(expectedAuthors[3].LastName, actualAuthor.LastName);
            Assert.AreEqual(expectedAuthors[3].IsDeleted, actualAuthor.IsDeleted);
        }


        [TestMethod]
        public async Task CanDeleteAuthorsAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedAuthors = new List<AuthorDto>()
            {
                new AuthorDto
                {
                    Id = 1,
                    FirstName = "Stephen",
                    LastName = "King",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 2,
                    FirstName = "Frank",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 3,
                    FirstName = "Brian",
                    LastName = "Herbert",
                    IsDeleted = false
                }
            };

            // Act
            var deleteMe = new AuthorDto
            {
                Id = 3,
                FirstName = "Brian",
                LastName = "Herbert",
                IsDeleted = true,
                DeletedByUserId = 1,
                DeletedOn = DateTime.UtcNow
            };
            await controller.DeleteAuthorAsync(deleteMe);
            var authors = await controller.GetActiveAuthorsAsync();
            var actualAuthors = authors.ToList();

            // Assert
            Assert.IsNull(actualAuthors.FirstOrDefault(z => z.FirstName == "Brian" && z.LastName == "Herbert"));
            Assert.IsNotNull(actualAuthors);
            Assert.AreEqual(expectedAuthors.Count - 1, actualAuthors.Count);
            for (int i = 0; i < expectedAuthors.Count - 1; i++)
            {
                Assert.AreEqual(expectedAuthors[i].Id, actualAuthors[i].Id);
                Assert.AreEqual(expectedAuthors[i].FirstName, actualAuthors[i].FirstName);
                Assert.AreEqual(expectedAuthors[i].LastName, actualAuthors[i].LastName);
                Assert.AreEqual(expectedAuthors[i].IsDeleted, actualAuthors[i].IsDeleted);
            }
        }
    }
}
