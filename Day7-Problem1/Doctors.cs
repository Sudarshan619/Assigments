using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_Problem1
{
    internal class Doctors
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Specialization { get; set; }

        public Doctors CreateDoctor()
        {
            Doctors doctors = new Doctors();
            Console.WriteLine("Enter the doctor id");
            doctors.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the doctor name");
            doctors.Name = Console.ReadLine(); ;
            Console.WriteLine("Enter the doctor gender");
            doctors.Gender = Console.ReadLine(); ;
            Console.WriteLine("Enter the doctor specialization");
            doctors.Specialization = Console.ReadLine();

            return doctors;
        }

    }


}
