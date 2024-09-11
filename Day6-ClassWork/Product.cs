using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork
{
    internal class Product
    {
        public int Id { get; set; } = 0;
        private static int productIdCounter = 0;
        public string Name { get; set; }
        public double Price { get; set; }

        public int Quantity { get; set; }

        
        public Product CreateProduct()
        {
            
            Product product = new Product();
            product.Id = ++productIdCounter;
            Console.WriteLine("Enter the name of the Product");
            product.Name = Console.ReadLine();
            Console.WriteLine("Enter the Price of the Product");
            product.Price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter the quantity of the Product");
            product.Quantity = Convert.ToInt32(Console.ReadLine());

            return product;
        }

        
    }
}
