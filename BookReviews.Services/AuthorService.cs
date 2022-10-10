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
    public class AuthorService : IAuthorService
    {
        private readonly BookReviewDataContext _context;
        public AuthorService(BookReviewDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorDto>> GetActiveAuthorsAsync()
        {
            var authors = from author in _context.Authors
                          where author.IsDeleted == false
                          select new AuthorDto
                          {
                              Id = author.Id,
                              FirstName = author.FirstName,
                              LastName = author.LastName,
                              IsDeleted = author.IsDeleted,
                              DeletedByUserId = author.DeletedByUserId,
                              DeletedOn = author.DeletedOn
                          };
            return await authors.ToListAsync();
        }
    }
}
