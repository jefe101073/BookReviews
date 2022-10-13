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
    public class ReviewService : IReviewService
    {
        private readonly BookReviewDataContext _context;
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public ReviewService(BookReviewDataContext context, IAuthorService authorService, IBookService bookService)
        {
            _context = context;
            _authorService = authorService;
            _bookService = bookService;
        }

        public async Task<IEnumerable<ReviewDto>> GetActiveReviewsAsync()
        {
            var reviews = await (from review in _context.Reviews
                                 join user in _context.Users on review.UserId equals user.Id
                                 join book in _context.Books on review.BookId equals book.Id
                                 join starrating in _context.StarRatings on review.StarRatingId equals starrating.Id
                                 join author in _context.Authors on book.AuthorId equals author.Id
                                 where book.IsDeleted == false
                                 select new ReviewDto
                                 {
                                     Id = book.Id,
                                     UserId = review.UserId,
                                     UserFirstName = user.FirstName,
                                     UserLastName = user.LastName,
                                     BookId = book.Id,
                                     BookTitle = book.Title,
                                     AuthorFirstName = author.FirstName,
                                     AuthorLastName = author.LastName,
                                     StarRatingId = review.StarRatingId,
                                     StarRatingValue = starrating.Value,
                                     StarRatingText = starrating.Text,
                                     DateBookWasRead = review.DateBookWasRead
                                 }).ToListAsync();

            return reviews;


        }
    }
}
