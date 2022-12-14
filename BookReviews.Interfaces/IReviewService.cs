using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReviews.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetActiveReviewsAsync();
    }
}
