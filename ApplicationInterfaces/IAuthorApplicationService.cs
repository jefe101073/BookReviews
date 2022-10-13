using BookReviews.Models.Dto;

namespace ApplicationInterfaces
{
    public interface IAuthorApplicationService
    {
        Task<AuthorDto?> AddAuthorAsync(AddAuthorDto addAuthorDto);
        Task<AuthorDto?> DeleteAuthorAsync(AuthorDto authorDto, int currentUserId);
        Task<List<AuthorDto>?> GetAllActiveAuthors();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto?> SaveAuthorAsync(AuthorDto authorDto);
    }
}