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

        public async Task<AuthorDto?> AddAuthorAsync(AddAuthorDto addAuthorDto)
        {
            var author = new Author { 
                LastName = addAuthorDto.LastName,
                FirstName = addAuthorDto.FirstName,
                IsDeleted = false,
                DeletedByUserId = null,
                DeletedOn = null
            };
            await _context.AddAsync(author);
            await _context.SaveChangesAsync();
            return new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                IsDeleted = author.IsDeleted,
                DeletedByUserId = author.DeletedByUserId,
                DeletedOn = author.DeletedOn
            };
        }

        public async Task<AuthorDto?> DeleteAuthorAsync(AuthorDto authorDto)
        {
            // Assume the deleted by info is set..
            var author = _context.Authors.FirstOrDefault(z => z.Id == authorDto.Id);
            if(author != null)
            {
                author.Id = authorDto.Id;
                author.FirstName = authorDto.FirstName;
                author.LastName = authorDto.LastName;
                author.IsDeleted = true;
                author.DeletedByUserId = authorDto.DeletedByUserId;
                author.DeletedOn = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();

            return authorDto;
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

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var a = from author in _context.Authors
                    where author.Id == id
                    select new AuthorDto
                    {
                        Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName,
                        IsDeleted = author.IsDeleted,
                        DeletedByUserId = author.DeletedByUserId,
                        DeletedOn = author.DeletedOn
                    };
            return await a.FirstOrDefaultAsync();
        }

        public async Task<AuthorDto?> SaveAuthorAsync(AuthorDto authorDto)
        {
            var authorInDb = _context.Authors.FirstOrDefault(z => z.Id == authorDto.Id);
            if(authorInDb != null)
            {
                authorInDb.FirstName = authorDto.FirstName;
                authorInDb.LastName = authorDto.LastName;

                await _context.SaveChangesAsync();
                return authorDto;
            }
            return null;
        }
    }
}
