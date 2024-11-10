using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nekrasova.Models;
using ServiceStack;
using ServiceStack.Host;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace Nekrasova.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public async Task<IActionResult> Catalog()
        {
            var viewModel = new BookInfo();
            viewModel.books = _appDbContext.Book.ToList();
            viewModel.author = _appDbContext.Author.ToList();
            viewModel.genre = _appDbContext.Genre.ToList();
            viewModel.publishingHouse = _appDbContext.PublishingHouse.ToList();
            viewModel.bookAuthor = _appDbContext.BookAuthor.ToList();
            viewModel.bookGenres = _appDbContext.BookGenre.ToList();
            viewModel.bookPBH = _appDbContext.BookPBH.ToList();
            viewModel.category = _appDbContext.Category.ToList();
            viewModel.language = _appDbContext.Language.ToList();
            viewModel.DbContext = _appDbContext;

            return View(viewModel);
            //return View(await _appDbContext.Book.ToListAsync());

        }

        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult Favorite()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public async Task<IActionResult> Goods()
        {
			var viewModel = new BookInfo();
			viewModel.books = _appDbContext.Book.ToList();
			viewModel.author = _appDbContext.Author.ToList();
			viewModel.genre = _appDbContext.Genre.ToList();
			viewModel.publishingHouse = _appDbContext.PublishingHouse.ToList();
			viewModel.bookAuthor = _appDbContext.BookAuthor.ToList();
			viewModel.bookGenres = _appDbContext.BookGenre.ToList();
			viewModel.bookPBH = _appDbContext.BookPBH.ToList();
			viewModel.category = _appDbContext.Category.ToList();
			viewModel.language = _appDbContext.Language.ToList();
			viewModel.DbContext = _appDbContext;
			return View(viewModel);
        }

        public IActionResult SignIn()
        {
            if (HttpContext.Session.Keys.Contains("AuthUser"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

       


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                LogPass user = await _appDbContext.LogPass.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("AuthUser", model.Login);
					HttpContext.Session.SetString("UserRole", user.Role_ID.ToString());
					await Authenticate(model.Login);
					return RedirectToAction("Profile", "Home");

				}
                ModelState.AddModelError("", "Некоректный логин или пароль");

            }
            return RedirectToAction("SignIn", "Home");
        }

        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, 
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("SignIn");
        }

        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(LogPass user)
        {
            user.Role_ID = 1;
            if (ModelState.IsValid)
            {
				if (await _appDbContext.LogPass.AnyAsync(u => u.Login == user.Login))
				{
					ModelState.AddModelError("", "Пользователь с таким email уже существует.");
					return View(user);
				}
				await _appDbContext.LogPass.AddAsync(user);
				await _appDbContext.SaveChangesAsync();
                return RedirectToAction("SignIn");
            }
                return View(user);
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
        
        public async Task<IActionResult> Profile()
        {
			var login = HttpContext.Session.GetString("AuthUser");
			if (login == null)
			{
				return RedirectToAction("SignIn");
			}


			var user = await _appDbContext.LogPass.FirstOrDefaultAsync(u => u.Login == login);

			if (user == null)
			{
				return NotFound();
			}



			var role = await _appDbContext.UserRole.FirstOrDefaultAsync(r => r.ID == user.Role_ID);
            User person = await _appDbContext.User.FirstOrDefaultAsync(p => p.LogPass_ID == user.ID);
            if (person == null)
            {
                ViewBag.Name = user.Login;
            }
            else {
                ViewBag.Name = person.Name;
            }


			if (role.RoleName == "Администратор")
			{
				ViewBag.UserRole = "Администратор";
				return View("Admin");
			}
			else if (role.RoleName == "Менеджер")
			{
				ViewBag.UserRole = "Менеджер";
				return View("Manager");
			}
			else
			{
				ViewBag.UserRole = "Покупатель";
				return View("Profile");
			}
		}

	}
}

