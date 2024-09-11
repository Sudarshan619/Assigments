using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork
{
    internal class Supplier
    {
        public string Name { get; set; }
        public int Id { get; set; }

     
        public Supplier CreateSupplier() { 
           Supplier supplier = new Supplier();

            Console.WriteLine("Enter supplier Id");
            supplier.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter supplier Name");
            supplier.Name = Console.ReadLine();
            

            return supplier;
        }
    }
}
