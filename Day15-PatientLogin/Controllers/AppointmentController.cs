using Microsoft.AspNetCore.Mvc;
using Day15_PatientLogin.Models;

namespace Day15_PatientLogin.Controllers
{
    public class AppointmentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            Patient patient = new Patient();
            var username = HttpContext.Session.GetString("username");
            ViewBag.Username = username;
            return View(patient);
        }
    }
}
