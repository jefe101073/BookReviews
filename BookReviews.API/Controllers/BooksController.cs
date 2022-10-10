using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using BookReviews.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
