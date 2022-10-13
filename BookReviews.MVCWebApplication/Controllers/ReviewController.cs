using ApplicationInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.MVCWebApplication.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReviewApplicationService _reviewApplicationService;

        public ReviewController(ILogger<HomeController> logger, IReviewApplicationService reviewApplicationService)
        {
            _logger = logger;
            _reviewApplicationService = reviewApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewApplicationService.GetAllActiveReviews();
            if (reviews == null)
            {
                ViewBag.ErrorMessage = "Cannot find any reviews.  Please check that the API is up and running.";
                return View();
            }
            return View(reviews.OrderBy(z => z.BookTitle).ToList());
        }
    }
}
