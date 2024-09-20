using Day13_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day13_WebApplication.Controllers
{
	public class ProductController : Controller
	{
        static List<Product> products = new List<Product>() {

                new Product { Id = 1, Name = "Pencil", Quantity = 10, Price = 10, Image = "/Images/Pencil.png" },
                new Product { Id = 2, Name = "Pen", Quantity = 20, Price = 20, Image = "/Images/Pen.jpg" }

         };
        public IActionResult Index()
		{
			
			return View(products);
		}

		[HttpGet]
		public IActionResult Create()
		{
			Product product = new Product();
			return View(product);
		}

		[HttpPost]
        public IActionResult Create(Product product)
        {
			product.Id = 1;
			product.Image = $"/Images/{product.Image}";
			products.Add(product);
            return RedirectToAction("Index");
        }

		[HttpGet]
		public IActionResult Edit(int id)
		{
			Product product = products.FirstOrDefault(x => x.Id==id);
			return View(product);
		}

		[HttpPost]
        public IActionResult Edit(int id,Product product)
        {
            Product oldproduct = products.FirstOrDefault(x => x.Id == id);
			oldproduct.Quantity= product.Quantity;
			oldproduct.Image = $"/Images/{product.Image}";
			oldproduct.Price = product.Price;
			oldproduct.Quantity = product.Quantity;

            return RedirectToAction("Index");
        }

		[HttpGet]
		public IActionResult Delete(int id)
		{
			Product product = products.FirstOrDefault(e => e.Id == id);

            return View(product);
		}
        [HttpPost]
        public IActionResult Delete(int id,Product prod)
        {
            Product oldproduct = products.FirstOrDefault(e => e.Id == id);
            if (oldproduct != null)
            {
                products.Remove(oldproduct);
            }

            return RedirectToAction("Index");
        }
    }
}
