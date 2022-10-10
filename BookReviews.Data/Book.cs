using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        public double? StarRating { get; set; } // Calculated value based on average reviews, value will update when review is added.
        public int? DeletedByUserId { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
