using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Interfaces
{
    public interface IBookService
    {
        Task<BookDto?> AddBookAsync(AddBookDto addBookDto);
        Task<BookDto?> DeleteBookAsync(BookDto bookDto);
        Task<IEnumerable<BookDto>> GetActiveBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto?> SaveBookAsync(BookDto bookDto);
    }
}
