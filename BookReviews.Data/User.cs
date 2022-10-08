using System.ComponentModel.DataAnnotations;

namespace BookReviews.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        public int? DeletedByUserId { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}