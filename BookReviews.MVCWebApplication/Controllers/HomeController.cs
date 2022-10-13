using ApplicationInterfaces;
using BookReviews.Models.Dto;
using BookReviews.MVCWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace BookReviews.MVCWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserApplicationService _userApplicationService;

        public HomeController(ILogger<HomeController> logger, IUserApplicationService userApplicationService)
        {
            _logger = logger;
            _userApplicationService = userApplicationService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(UserDto objUser)
        {
            if (ModelState.IsValid)
            {
                if (objUser.Email == null || objUser.Password == null)
                {
                    return View();
                }

                var authenticated = await _userApplicationService.AuthenticateUserAsync(objUser.Email, objUser.Password);

                if (authenticated != null)
                {
                    HttpContext.Session.SetInt32("UserID", authenticated.Id);
                }

                return RedirectToAction("Index");
            }
            return View(objUser);
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                HttpContext.Session.SetInt32("UserID", (int)userId);
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}