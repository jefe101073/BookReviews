using BookReviews.Interfaces;
using BookReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.API.Controllers
{
    /// <summary>
    /// This is the ReviewsController.  It contains all the endpoints for the book Reviews functionality.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {
        public IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Gets Authors who do not have the IsDeleted flag enabled.
        /// </summary>
        /// <returns>IEnumerable list of AuthorDtos</returns>
        [HttpGet]
        public async Task<IEnumerable<ReviewDto>> GetActiveUsersAsync() => await _reviewService.GetActiveReviewsAsync();

    }
}
