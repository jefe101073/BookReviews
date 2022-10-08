using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jeff",
                    LastName = "McCann",
                    Email = "jefe101073@gmail.com",
                    IsDeleted = false,
                    Password = DataHelpers.PasswordEncrypt("password")
                }
            );
            modelBuilder.Entity<StarRating>().HasData(
                new StarRating { Id = 1, Text = "*", Value = "Poor" },
                new StarRating { Id = 2, Text = "**", Value = "Moderate" },
                new StarRating { Id = 3, Text = "***", Value = "Good" },
                new StarRating { Id = 4, Text = "****", Value = "Very Good" },
                new StarRating { Id = 5, Text = "*****", Value = "Excellent" }
                );
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "Stephen",
                    LastName = "King",
                    IsDeleted = false
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Frank",
                    LastName = "Herbert",
                    IsDeleted = false
                },
                new Author
                {
                    Id = 1,
                    FirstName = "Brian",
                    LastName = "Herbert",
                    IsDeleted = false
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new Book
                {
                    Id = 1,
                    Title = "Book 2",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                },
                new Book
                {
                    Id = 1,
                    Title = "Book 3",
                    AuthorId = 1,
                    NumberOfPages = 400,
                    IsDeleted = false,
                    StarRating = 2,
                }
            );
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    UserId = 1,
                    BookId = 1,
                    StarRatingId = 4
                },
                new Review
                {
                    Id = 2,
                    UserId = 1,
                    BookId = 2,
                    StarRatingId = 3
                },
                new Review
                {
                    Id = 3,
                    UserId = 1,
                    BookId = 3,
                    StarRatingId = 5
                }
            );
        }
    }
}
