using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.API.Controllers
{
    /// <summary>
    /// This is the UsersController.  It contains all the endpoints for user functionality.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// The UsersContoller constructor takes in the User Dao so that it can be used to access the data layer.
        /// </summary>
        /// <param name="userDao"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets users who do not have the IsDeleted flag enabled.
        /// </summary>
        /// <returns>IEnumerable list of UserDtos</returns>
        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetActiveUsersAsync() => await _userService.GetActiveUsersAsync();

        /// <summary>
        /// Gets a specific user by Id
        /// </summary>
        /// <remarks>
        /// GetUser will return a user even if it is blocked or deleted.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A single UserDto or Null if not found.</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<UserDto?> GetUserAsync(int id) => await _userService.GetUserAsync(id);

        /// <summary>
        /// Adds a user to the database
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown when User's email already exists.</exception>
        /// <param name="user"></param>
        /// <returns>The UserDto that was passed in.</returns>
        [HttpPost]
        [Route("")]
        public async Task<UserDto?> AddUserAsync(AddUserDto user) => await _userService.AddUserAsync(user);

        /// <summary>
        /// Deletes a user by setting the IsDeleted flag, DeletedByUserId and DeletedOn values.  Deleted users cannot submit reviews or add restaurants
        /// </summary>
        /// <exception cref="ObjectNotFoundException">Thrown when checking for user by Id and it doesn't exist.</exception>
        /// <param name="id"></param>
        /// <param name="currentUserId"></param>
        [HttpDelete]
        [Route("{id}/{currentUserId}")]
        public async Task DeleteUserAsync(int id, int currentUserId) => await _userService.DeleteUserAsync(id, currentUserId);

        /// <summary>
        /// Authenticates user by checking email address and password.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("authenticate/{email}/{password}")]
        public async Task<bool> AuthenticateUserAsync(string email, string password) => await _userService.AuthenticateUserAsync(email, password);

        /// <summary>
        /// Checks if a user has IsDeleted or IsUserBlocked flag and returns boolean
        /// </summary>
        /// <exception cref="ObjectNotFoundException">Thrown when checking for user by Id and it doesn't exist.</exception>
        /// <param name="id"></param>
        [HttpPost]
        [Route("isdeleted/{id}")]
        public async Task<bool> IsUserBlockedOrDeletedAsync(int id) => await _userService.IsUserDeletedAsync(id);

        /// <summary>
        /// Unsets the IsDeleted flag to undelete users
        /// </summary>
        /// <exception cref="ObjectNotFoundException">Thrown when checking for user by Id and it doesn't exist.</exception>
        /// <param name="id"></param>
        [HttpPost]
        [Route("undelete/{id}")]
        public async Task UndeleteUserAsync(int id) => await _userService.UndeleteUserAsync(id);

    }
}
