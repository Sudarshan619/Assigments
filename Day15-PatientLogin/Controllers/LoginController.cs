using Day15_PatientLogin.Service;
using Microsoft.AspNetCore.Mvc;
using Day15_PatientLogin.Models;

namespace Day15_PatientLogin.Controllers
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
            Patient user = new Patient();
            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword(Patient user)
        {
            return View(user);
        }

        [HttpPost]
        public IActionResult ChangePassword(Patient user, int id)
        {
            var updatedUser = _loginService.ChangePassword(user);

            if (updatedUser != null)
            {

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Password change failed user does not exist");

            return View(user);
        }


        [HttpPost]
        public IActionResult Login(Patient user, int id)
        {
            var loginUser = _loginService.Login(user.UserName, user.Password);

            if (loginUser != null)
            {
                HttpContext.Session.SetString("username", loginUser.UserName);
                
                
                return RedirectToAction("Index", "Appointment");
            }
            ModelState.AddModelError("", "User not found");
            return View("Index");
        }

        [HttpGet]
        public IActionResult PassWordChange(Patient user)
        {
            return View(user);
        }
    }
}
