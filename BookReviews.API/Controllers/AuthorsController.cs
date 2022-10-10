using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using BookReviews.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.API.Controllers
{
    /// <summary>
    /// This is the AuthorsController.  It contains all the endpoints for author functionality.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        public IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Gets Authors who do not have the IsDeleted flag enabled.
        /// </summary>
        /// <returns>IEnumerable list of AuthorDtos</returns>
        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetActiveUsersAsync() => await _authorService.GetActiveAuthorsAsync();
    }
}
