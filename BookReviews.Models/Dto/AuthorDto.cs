using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Models.Dto
{
    public class AddAuthorDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
    public class AuthorDto : AddAuthorDto
    {
        public bool IsDeleted { get; set; } = false;
        public int? DeletedByUserId { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
