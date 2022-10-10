using BookReviews.Data;
using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Services
{
    public class BookService : IBookService
    {
        private readonly BookReviewDataContext _context;
        public BookService(BookReviewDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookDto>> GetActiveBooksAsync()
        {
            var books = from book in _context.Books
                          where book.IsDeleted == false
                          select new BookDto
                          {
                              Id = book.Id,
                              AuthorId = book.AuthorId,
                              NumberOfPages = book.NumberOfPages,
                              StarRating = book.StarRating,
                              Title = book.Title,
                              IsDeleted = book.IsDeleted,
                              DeletedByUserId = book.DeletedByUserId,
                              DeletedOn = book.DeletedOn
                          };
            return await books.ToListAsync();
        }
    }
}
