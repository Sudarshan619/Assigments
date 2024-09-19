using Microsoft.AspNetCore.Mvc;
using Day12_WebApplication.Models;

namespace Day12_WebApplication.Controllers
{
    
    public class FirstController : Controller
    {

        public IActionResult Profile()
        {
            ViewBag.Customer = new Customer(1,"Sudu",20);
            return View();
        }

        public IActionResult CustomerData() {
             Customer customer = new Customer(1,"Sudu", 20);
             return View(customer); 
        
        }
    }
}
