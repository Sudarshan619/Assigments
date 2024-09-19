using Microsoft.AspNetCore.Mvc;
using Day12_WebApplication.Models;

namespace Day12_WebApplication.Controllers
{
    public class ProductController : Controller
    {

        List<Product> products = new List<Product>() {

          new Product{ Id = 1, Name = "Pencil", Price = 10, Description = "Used for drawing", Image = "/Images/Pen.jpg", Quantity = 10 },
            new Product{ Id = 2, Name = "Pen", Price = 20, Description = "Used for writing", Image = "/Images/Pencil.png", Quantity = 20 }

        };
        public IActionResult Index()
        {
            return View(products);
        }
    }
}
