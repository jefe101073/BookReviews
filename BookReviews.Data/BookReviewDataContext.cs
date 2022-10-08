using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Data
{
    public class BookReviewDataContext : DbContext
    {
        public BookReviewDataContext(DbContextOptions<BookReviewDataContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Seed();
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<StarRating>? StarRatings { get; set; }

    }
}
