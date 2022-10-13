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
        private readonly IAuthorService _authorService;
        public BookService(BookReviewDataContext context, IAuthorService authorService)
        {
            _context = context;
            _authorService = authorService; 
        }

        public async Task<BookDto?> AddBookAsync(AddBookDto addBookDto)
        {
            var authorInfo = await GetAuthorInfoAsync(addBookDto);

            var book = new Book
            {
                Title = addBookDto.Title,
                AuthorId = authorInfo.AuthorId,
                IsDeleted = false,
                DeletedByUserId = null,
                DeletedOn = null
            };
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorFirstName = authorInfo.AuthorFirstName,
                AuthorLastName = authorInfo.AuthorLastName,
                StarRating = authorInfo.StarRating,
                IsDeleted = book.IsDeleted,
                DeletedByUserId = book.DeletedByUserId,
                DeletedOn = book.DeletedOn
            };
        }

        public async Task<BookDto?> DeleteBookAsync(BookDto bookDto)
        {
            // Assume the deleted by info is set..
            var book = _context.Books.FirstOrDefault(z => z.Id == bookDto.Id);
            if (book != null)
            {
                book.Id = bookDto.Id;
                book.Title = bookDto.Title;
                book.IsDeleted = true;
                book.DeletedByUserId = bookDto.DeletedByUserId;
                book.DeletedOn = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();

            return bookDto;
        }

        public async Task<IEnumerable<BookDto>> GetActiveBooksAsync()
        {
            var books = from book in _context.Books
                        join author in _context.Authors on book.AuthorId equals author.Id
                        where book.IsDeleted == false
                        select new BookDto
                        {
                            Id = book.Id,
                            AuthorId = book.AuthorId,
                            NumberOfPages = book.NumberOfPages,
                            StarRating = book.StarRating,
                            AuthorFirstName = author.FirstName != null ? author.FirstName : "",
                            AuthorLastName = author.LastName != null ? author.LastName : "",
                            Title = book.Title,
                            IsDeleted = book.IsDeleted,
                            DeletedByUserId = book.DeletedByUserId,
                            DeletedOn = book.DeletedOn
                        };
            return await books.ToListAsync();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var bk = from book in _context.Books
                    join author in _context.Authors on book.AuthorId equals author.Id
                    where book.Id == id
                    select new BookDto
                    {
                        Id = book.Id,
                        AuthorFirstName = author.FirstName,
                        AuthorLastName = author.LastName,
                        Title = book.Title,
                        StarRating = book.StarRating,
                        IsDeleted = book.IsDeleted,
                        DeletedByUserId = book.DeletedByUserId,
                        DeletedOn = book.DeletedOn
                    };
            return await bk.FirstOrDefaultAsync();
        }

        public async Task<BookDto?> SaveBookAsync(BookDto bookDto)
        {
            var authorInfo = await GetAuthorInfoAsync(bookDto);

            var bookInDb = _context.Books.FirstOrDefault(z => z.Id == bookDto.Id);
            if (bookInDb != null)
            {
                bookInDb.Title = bookDto.Title;
                bookInDb.AuthorId = authorInfo.AuthorId;
                bookDto.AuthorFirstName = authorInfo.AuthorFirstName;
                bookDto.AuthorLastName = authorInfo.AuthorLastName;
                bookDto.StarRating = bookDto.StarRating;
                await _context.SaveChangesAsync();
                return bookDto;
            }
            return null;
        }

        /// <summary>
        ///  used to get author info from the book's AuthorId
        /// </summary>
        /// <param name="addBookDto"></param>
        /// <returns></returns>
        private async Task<BookDto?> GetAuthorInfoAsync(AddBookDto addBookDto)
        {
            int? authorId = null;
            string? authorFirstName = null;
            string? authorLastName = null;

            var authorCheck = _context.Authors.FirstOrDefault(z =>
                                                z.FirstName != null && z.FirstName.ToLower().Equals(addBookDto.AuthorFirstName.ToLower()) &&
                                                z.LastName != null && z.LastName.ToLower().Equals(addBookDto.AuthorLastName.ToLower()));
            if (authorCheck != null)
            {
                authorId = authorCheck.Id;
                authorFirstName = authorCheck.FirstName;
                authorLastName = authorCheck.LastName;
            }
            else
            {
                // need to add author
                var author = await _authorService.AddAuthorAsync(new AddAuthorDto
                {
                    FirstName = addBookDto.AuthorFirstName,
                    LastName = addBookDto.AuthorLastName
                });
                if (author != null)
                {
                    authorId = author.Id;
                    authorFirstName = author.FirstName;
                    authorLastName = author.LastName;
                }
            }

            var authorInfo = new BookDto
            {
                AuthorId = authorId != null ? (int)authorId : 0,
                AuthorFirstName = authorFirstName,
                AuthorLastName = authorLastName
            };
            return authorInfo;
        }
    }
}
