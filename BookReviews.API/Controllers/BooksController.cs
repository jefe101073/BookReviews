using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using BookReviews.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core;

namespace BookReviews.API.Controllers
{
    /// <summary>
    /// This is the BooksController.  It contains all the endpoints for book functionality.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets Books who do not have the IsDeleted flag enabled.
        /// </summary>
        /// <returns>IEnumerable list of BookDtos</returns>
        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetActiveUsersAsync() => await _bookService.GetActiveBooksAsync();

        /// <summary>
        /// Gets Book by id.
        /// </summary>
        /// <returns>a single BookDto</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<BookDto?> GetBookByIdAsync(int id) => await _bookService.GetBookByIdAsync(id);

        /// <summary>
        /// Adds an book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<BookDto?> AddBookAsync([FromBody] AddBookDto addBookDto) => await _bookService.AddBookAsync(addBookDto);

        /// <summary>
        /// Saves an existing book
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<BookDto?> SaveBookAsync([FromBody] BookDto bookDto) => await _bookService.SaveBookAsync(bookDto);

        /// <summary>
        /// Deletes a user by setting the IsDeleted flag, DeletedByUserId and DeletedOn values.  Deleted users cannot submit reviews or add restaurants
        /// </summary>
        /// <exception cref="ObjectNotFoundException">Thrown when checking for user by Id and it doesn't exist.</exception>
        /// <param name="id"></param>
        /// <param name="currentUserId"></param>
        [HttpPost]
        [Route("delete")]
        public async Task DeleteBookAsync([FromBody] BookDto bookDto) => await _bookService.DeleteBookAsync(bookDto);
    }
}
