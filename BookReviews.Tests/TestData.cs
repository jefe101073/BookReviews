using BookReviews.Data;
using BookReviews.Models.Dto;
using BookReviews.Data;
using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookReviews.Tests.DataForTests
{
    public static class TestData
    {
        public static List<AuthorDto> AuthorList;
        public static List<BookDto> BookList;
        public static List<UserDto> UserList;
        public static List<StarRatingDto> StarRatings;
        public static List<ReviewDto> ReviewList;

        static TestData()
        {
            AuthorList = new List<AuthorDto>();
            BookList = new List<BookDto>();
            UserList = new List<UserDto>();
            StarRatings = new List<StarRatingDto>();
            ReviewList = new List<ReviewDto>();
        }

        public static void LoadData()
        {
            UserList = new List<UserDto> {
                new UserDto
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                },
                new UserDto
                {
                    Id = 2,
                    FirstName = "Jeff",
                    LastName = "McCann",
                    Email = "jefe101073@gmail.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                }
           };
            StarRatings = new List<StarRatingDto> {
                new StarRatingDto { Id = 1, Text = "*", Value = "Poor" },
                new StarRatingDto { Id = 2, Text = "**", Value = "Moderate" },
                new StarRatingDto { Id = 3, Text = "***", Value = "Good" },
                new StarRatingDto { Id = 4, Text = "****", Value = "Very Good" },
                new StarRatingDto { Id = 5, Text = "*****", Value = "Excellent" }
                };
            AuthorList = new List<AuthorDto> {
                new AuthorDto
                {
                    Id = 1,
                    FirstName = "Stephen",
                    LastName = "King",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 2,
                    FirstName = "Frank",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new AuthorDto
                {
                    Id = 3,
                    FirstName = "Brian",
                    LastName = "Herbert",
                    IsDeleted = false
                }
            };
            BookList = new List<BookDto> {
                new BookDto
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 2,
                    Title = "Book 2",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new BookDto
                {
                    Id = 3,
                    Title = "Book 3",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                }
            };
            ReviewList = new List<ReviewDto> {
                new ReviewDto
                {
                    Id = 1,
                    UserId = 1,
                    BookId = 1,
                    StarRatingId = 4
                },
                new ReviewDto
                {
                    Id = 2,
                    UserId = 1,
                    BookId = 2,
                    StarRatingId = 3
                },
                new ReviewDto
                {
                    Id = 3,
                    UserId = 1,
                    BookId = 3,
                    StarRatingId = 5
                }
            };

        }
    }
}
