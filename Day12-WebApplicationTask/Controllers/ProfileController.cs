using Microsoft.AspNetCore.Mvc;
using Day12_WebApplicationTask.Models;

namespace Day12_WebApplicationTask.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            Profile profile = new Profile();
            
            return View(profile);
        }
    }
}
