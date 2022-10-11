using BookReviews.Models.Dto;

namespace ApplicationInterfaces
{
    public interface IAuthorApplicationService
    {
        Task<List<AuthorDto>?> GetAllActiveAuthors();
    }
}