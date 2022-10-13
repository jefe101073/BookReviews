using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Models.Dto
{
    public class AddReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }
        public int StarRatingId { get; set; } // Int based on values *, **, ***, ****, *****
        public string? StarRatingValue { get; set; } // values, such as "*", "**", "***", "****" and "*****"
        public string? StarRatingText { get; set; }
        public DateTime DateBookWasRead { get; set; }
    }
    public class ReviewDto : AddReviewDto
    {
        public bool IsDeleted { get; set; } = false;
        public int? DeletedByUserId { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
