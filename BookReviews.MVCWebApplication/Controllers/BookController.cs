using ApplicationInterfaces;
using ApplicationServices;
using BookReviews.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BookReviews.MVCWebApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookApplicationService _bookApplicationService;

        public BookController(ILogger<HomeController> logger, IBookApplicationService bookApplicationService)
        {
            _logger = logger;
            _bookApplicationService = bookApplicationService;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookApplicationService.GetAllActiveBooks();
            if (books == null)
            {
                ViewBag.ErrorMessage = "Cannot find any books.  Please check that the API is up and running.";
                return View();
            }
            return View(books.OrderBy(z => z.Title).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookApplicationService.GetBookByIdAsync(id);
            if (book == null)
            {
                ViewBag.ErrorMessage = "Cannot find book.";
                return View();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookDto bookDto)
        {
            var book = await _bookApplicationService.SaveBookAsync(bookDto);
            if (book == null)
            {
                ViewBag.ErrorMessage = "Cannot find book.";
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
        public async Task<IActionResult> Add(AddBookDto addBookDto)
        {
            var book = await _bookApplicationService.AddBookAsync(addBookDto);
            if (book == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
