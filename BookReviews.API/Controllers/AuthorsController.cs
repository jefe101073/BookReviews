using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using BookReviews.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity.Core;

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
        public async Task<IEnumerable<AuthorDto>> GetActiveAuthorsAsync() => await _authorService.GetActiveAuthorsAsync();

        /// <summary>
        /// Gets Author by id.
        /// </summary>
        /// <returns>a single AuthorDto</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<AuthorDto?> GetAuthorByIdAsync(int id) => await _authorService.GetAuthorByIdAsync(id);

        /// <summary>
        /// Adds an author
        /// </summary>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<AuthorDto?> AddAuthorAsync([FromBody] AddAuthorDto addAuthorDto) => await _authorService.AddAuthorAsync(addAuthorDto);

        /// <summary>
        /// Saves an existing author
        /// </summary>
        /// <param name="authorDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        public async Task<AuthorDto?> SaveAuthorAsync([FromBody] AuthorDto authorDto) => await _authorService.SaveAuthorAsync(authorDto);

        /// <summary>
        /// Deletes a user by setting the IsDeleted flag, DeletedByUserId and DeletedOn values.  Deleted users cannot submit reviews or add restaurants
        /// </summary>
        /// <exception cref="ObjectNotFoundException">Thrown when checking for user by Id and it doesn't exist.</exception>
        /// <param name="id"></param>
        /// <param name="currentUserId"></param>
        [HttpPost]
        [Route("delete")]
        public async Task DeleteAuthorAsync([FromBody] AuthorDto authorDto) => await _authorService.DeleteAuthorAsync(authorDto);

    }
}
