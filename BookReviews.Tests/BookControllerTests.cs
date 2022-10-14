
namespace BookReviews.Tests
{
    [TestClass]
    public class BookControllerTests
    {
        private Mock<IBookService> _bookServiceMock;

        public BookControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();
        }

        // Creates an instance of the controller so we can test
        private BooksController GetControllerInstance()
        {
            return new BooksController(_bookServiceMock.Object);
        }
        // runs every time a test starts
        [TestInitialize()]
        public void TestInitialize()
        {
            TestData.LoadData(); // Reset the test data

            // Mock functionality
            _bookServiceMock.Setup(x => x.GetActiveBooksAsync()).ReturnsAsync(TestData.BookList.Where(u => u.IsDeleted == false));

            _bookServiceMock.Setup(x => x.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync((int i) => TestData.BookList.FirstOrDefault(z => z.Id == i));

            _bookServiceMock.Setup(x => x.AddBookAsync(It.IsAny<BookDto>())).ReturnsAsync(
                (BookDto target) =>
                {
                    TestData.BookList.Add(target);
                    return target;
                });

            _bookServiceMock.Setup(x => x.DeleteBookAsync(It.IsAny<BookDto>())).Callback((BookDto bookDto) =>
            {
                var itemToUpdate = TestData.BookList.FirstOrDefault(u => u.Id == bookDto.Id);
                if (itemToUpdate == null) return;
                itemToUpdate.IsDeleted = true;
                itemToUpdate.DeletedOn = DateTime.UtcNow;
                itemToUpdate.DeletedByUserId = bookDto.DeletedByUserId;
                return;
            });
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            // reset mocks to prepare for the next test
            _bookServiceMock.Reset();
        }

        [TestMethod]
        public async Task CanGetActiveBooksAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedBooks = new List<BookDto>()
            {
                new BookDto
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 2,
                    Title = "Book 2",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 3,
                    Title = "Book 3",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                }
            };

            // Act
            var books = await controller.GetActiveBooksAsync();
            var actualBooks = books.ToList();

            // Assert
            Assert.IsNotNull(actualBooks);
            Assert.AreEqual(expectedBooks.Count, actualBooks.Count);
            for (int i = 0; i < expectedBooks.Count; i++)
            {
                Assert.AreEqual(expectedBooks[i].Id, actualBooks[i].Id);
                Assert.AreEqual(expectedBooks[i].Title, actualBooks[i].Title);
                Assert.AreEqual(expectedBooks[i].NumberOfPages, actualBooks[i].NumberOfPages);
                Assert.AreEqual(expectedBooks[i].StarRating, actualBooks[i].StarRating);
                Assert.AreEqual(expectedBooks[i].IsDeleted, actualBooks[i].IsDeleted);
            }
        }

        [TestMethod]
        public async Task CanGetBookByIdAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedBooks = new List<BookDto>()
            {
                new BookDto
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 2,
                    Title = "Book 2",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 3,
                    Title = "Book 3",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                }
            };

            // Act
            var actualBook = await controller.GetBookByIdAsync(2);

            // Assert
            Assert.IsNotNull(actualBook);

            Assert.AreEqual(expectedBooks[1].Id, actualBook.Id);
            Assert.AreEqual(expectedBooks[1].Title, actualBook.Title);
            Assert.AreEqual(expectedBooks[1].NumberOfPages, actualBook.NumberOfPages);
            Assert.AreEqual(expectedBooks[1].StarRating, actualBook.StarRating);
            Assert.AreEqual(expectedBooks[1].IsDeleted, actualBook.IsDeleted);
        }

        [TestMethod]
        public async Task CanAddBookAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedBooks = new List<BookDto>()
            {
                new BookDto
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 2,
                    Title = "Book 2",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 3,
                    Title = "Book 3",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 4,
                    Title = "Book 4",
                    AuthorId = 1,
                    NumberOfPages = 240,
                    IsDeleted = false,
                    StarRating = 3,
                }
            };

            // Act
            var addBook = await controller.AddBookAsync(expectedBooks[3]);
            var actualBook = await controller.GetBookByIdAsync(4);

            // Assert
            Assert.IsNotNull(actualBook);

            Assert.AreEqual(expectedBooks[3].Id, actualBook.Id);
            Assert.AreEqual(expectedBooks[3].Title, actualBook.Title);
            Assert.AreEqual(expectedBooks[3].NumberOfPages, actualBook.NumberOfPages);
            Assert.AreEqual(expectedBooks[3].StarRating, actualBook.StarRating);
            Assert.AreEqual(expectedBooks[3].IsDeleted, actualBook.IsDeleted);
        }


        [TestMethod]
        public async Task CanDeleteBooksAsync()
        {
            // Arrange
            var controller = GetControllerInstance();

            var expectedBooks = new List<BookDto>()
            {
                new BookDto
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 2,
                    Title = "Book 2",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 3,
                    Title = "Book 3",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                }
            };

            // Act
            var deleteMe = new BookDto
            {
                Id = 3,
                Title = "Book 3",
                AuthorId = 1,
                NumberOfPages = 400,
                StarRating = 2,
                IsDeleted = true,
                DeletedByUserId = 1,
                DeletedOn = DateTime.UtcNow
            };
            await controller.DeleteBookAsync(deleteMe);
            var books = await controller.GetActiveBooksAsync();
            var actualBooks = books.ToList();

            // Assert
            Assert.IsNull(actualBooks.FirstOrDefault(z => z.Title == "Book3"));
            Assert.IsNotNull(actualBooks);
            Assert.AreEqual(expectedBooks.Count - 1, actualBooks.Count);
            for (int i = 0; i < expectedBooks.Count - 1; i++)
            {
                Assert.AreEqual(expectedBooks[i].Id, actualBooks[i].Id);
                Assert.AreEqual(expectedBooks[i].Title, actualBooks[i].Title);
                Assert.AreEqual(expectedBooks[i].NumberOfPages, actualBooks[i].NumberOfPages);
                Assert.AreEqual(expectedBooks[i].StarRating, actualBooks[i].StarRating);
                Assert.AreEqual(expectedBooks[i].IsDeleted, actualBooks[i].IsDeleted);
            }
        }
    }
}
