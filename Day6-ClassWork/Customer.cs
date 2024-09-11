using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork
{
    internal class Customer
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public int Phone { get; set; }


        public Customer CreateCustomer()
        {
            Customer customer = new Customer();

            Console.WriteLine("Enter customer Name");
            var name = Console.ReadLine();
            customer.Name = name;
            Console.WriteLine("Enter customer Id");
            var id = Convert.ToInt32(Console.ReadLine());
            customer.Id = id;
            Console.WriteLine("Enter customer Phone number");
            var phone = Convert.ToInt32(Console.ReadLine());
            customer.Phone = phone;

            return customer;
        }
    }
}
