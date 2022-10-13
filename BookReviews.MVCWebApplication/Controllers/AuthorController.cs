using ApplicationInterfaces;
using BookReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.MVCWebApplication.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthorApplicationService _authorApplicationService;

        public AuthorController(ILogger<HomeController> logger, IAuthorApplicationService authorApplicationService)
        {
            _logger = logger;
            _authorApplicationService = authorApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _authorApplicationService.GetAllActiveAuthors();
            if(authors == null)
            {
                ViewBag.ErrorMessage = "Cannot find any authors.  Please check that the API is up and running.";
                return View();
            }
            return View(authors.OrderBy(z => z.LastName).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorApplicationService.GetAuthorByIdAsync(id);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorDto authorDto)
        {
            var author = await _authorApplicationService.SaveAuthorAsync(authorDto);
            if(author == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddAuthorDto addAuthorDto)
        {
            var author = await _authorApplicationService.AddAuthorAsync(addAuthorDto);
            if (author == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorApplicationService.GetAuthorByIdAsync(id);
            var currentUserId = HttpContext.Session.GetInt32("UserID");
            if (currentUserId == null || author == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var returnMe = await _authorApplicationService.DeleteAuthorAsync(author, id);
            return RedirectToAction("Index");
        }
    }
}
