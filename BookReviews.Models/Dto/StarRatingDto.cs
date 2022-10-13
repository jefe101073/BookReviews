using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Models.Dto
{
    public class AddStarRatingDto
    {
        public int Id { get; set; }
        public string? Value { get; set; } // example *, **, ***, ****, *****
        public string? Text { get; set; } // examples:
                                          // * = Poor
                                          // ** = Moderate
                                          // *** = Good
                                          // **** = Very Good
                                          // ***** = Excellent
    }

    public class StarRatingDto : AddStarRatingDto
    {

    }
}
