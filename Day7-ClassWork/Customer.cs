using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_ClassWork
{
    public class Customer
    {
        public string Name { get; set; } = "";
        public string Gender { get; set; } = "";

        public DateTime DOB { get; set; }

        public virtual void SetCustomerDetails()
        {
            Console.WriteLine("Enter the Customer name");
            Name = Console.ReadLine()??"";
            Console.WriteLine("Enter the Customer gender");
            Gender = Console.ReadLine()??"";
            Console.WriteLine("Enter the Customer DOB");
            DOB = DateTime.Parse(Console.ReadLine());

        }
    }
}
