using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInterfaces
{
    public interface IBookApplicationService
    {
        Task<BookDto?> AddBookAsync(AddBookDto addBookDto);
        Task<List<BookDto?>> GetAllActiveBooks();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task<BookDto?> SaveBookAsync(BookDto bookDto);
    }
}
