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
                if(objUser.Email == null || objUser.Password == null)
                {
                    return View();
                }

                var authenticated = await _userApplicationService.AuthenticateUserAsync(objUser.Email, objUser.Password);

                if (authenticated != null)
                {
                    HttpContext.Session.SetInt32("UserID", authenticated.Id);
                    HttpContext.Session.SetString("UserEmail", authenticated.Email);
                    HttpContext.Session.SetString("UserFirstName", authenticated.FirstName);
                    HttpContext.Session.SetString("UserLastName", authenticated.LastName);
                }

                return RedirectToAction("Privacy");
            }
            return View(objUser);
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId != null)
            {
                var lastName = HttpContext.Session.GetString("UserLastName");
                HttpContext.Session.SetInt32("UserID", (int)userId);
                return View();
            }
            else
            {
                return RedirectToAction("Login");
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