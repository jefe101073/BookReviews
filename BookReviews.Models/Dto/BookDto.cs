using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Models.Dto
{
    public class AddBookDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int AuthorId { get; set; }
        public int NumberOfPages { get; set; }
        public double? StarRating { get; set; } // Calculated value based on average reviews, value will update when review is added.
        
    }
    public class BookDto
    {
        public bool IsDeleted { get; set; } = false;
        public int? DeletedByUserId { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
