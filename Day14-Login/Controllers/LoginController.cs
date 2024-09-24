using Day14_Login.Services;
using Microsoft.AspNetCore.Mvc;
using Day14_Login.Models;

namespace Day14_Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            User user = new User();
            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword(User user)
        {
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangePassword(User user, int id)
        {
                var updatedUser = _loginService.ChangePassword(user);
                
                if (updatedUser != null)
                {
                
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Password change failed user does not exist");
            
            return View(user);
        }

        //[HttpGet]
        //public IActionResult Login(User user)
        //{
        //    return View(user);
        //}

        [HttpPost]
        public IActionResult Login(User user, int id)
        {
            var loginUser = _loginService.Login(user.UserName, user.Password);
            
            if (loginUser != null)
            {
                HttpContext.Session.SetString("username",loginUser.UserName);
                return RedirectToAction("Index", "Pizza");
            }
            ModelState.AddModelError("", "User not found");
            return View("Index");
        }

        [HttpGet]
        public IActionResult PassWordChange(User user)
        {
            return View(user);
        }
    }
}