using BookReviews.Models.Dto;

namespace BookReviews.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> AddUserAsync(AddUserDto user);
        Task<bool> AuthenticateUserAsync(string email, string password);
        Task DeleteUserAsync(int id, int currentUserId);
        Task<IEnumerable<UserDto>> GetActiveUsersAsync();
        Task<UserDto?> GetUserAsync(int id);
        Task<bool> IsUserDeletedAsync(int userId);
        Task UndeleteUserAsync(int id);
    }
}