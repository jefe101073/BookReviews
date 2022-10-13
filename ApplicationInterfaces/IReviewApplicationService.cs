using BookReviews.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationInterfaces
{
    public interface IReviewApplicationService
    {
        Task<List<ReviewDto>?> GetAllActiveReviews();
    }
}
