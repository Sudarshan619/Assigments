using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal class Student
    {
        public Student(int Id,string Name,DateTime DOB,long Phone,string Email)
        {
            this.Id = Id;
            this.DOB = DOB;
            this.Name = Name;
            this.Phone = Phone;
            this.Email = Email;
            
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
