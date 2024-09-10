using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class Student
    {
        public Student(int id, string name, DateTime dOB, long phone, string email)
        {
            Id = id;
            Name = name;
            DOB = dOB;
            Phone = phone;
            Email = email;
        }

        public int Id { get; set; }

        public string Name { get; set; } = "";
        public DateTime DOB { get; set; }
        
        public long Phone { get; set; }

        public string Email { get; set; } = "";

        public void DisplayId()
        {
            Console.WriteLine($"Id:{Id}");
            Console.WriteLine($"Name:{Name}");
            Console.WriteLine($"Date of Birth:{DOB}");
            Console.WriteLine($"Phone Number:{Phone}");
            Console.WriteLine($"Email:{Email}");
        }
    }
}
