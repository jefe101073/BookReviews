using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto?> AddAuthorAsync(AddAuthorDto addAuthorDto);
        Task<AuthorDto?> DeleteAuthorAsync(AuthorDto authorDto);
        Task<IEnumerable<AuthorDto>> GetActiveAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<AuthorDto?> SaveAuthorAsync(AuthorDto authorDto);
    }
}
