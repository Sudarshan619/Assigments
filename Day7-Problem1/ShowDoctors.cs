using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_Problem1
{
    public class ShowDoctors:IShowDoctors
    {
        Doctors[] doctors;
        static int size = 0; 
        public ShowDoctors() {

            Console.WriteLine("Enter the number of doctors you want to add");
            int count = Convert.ToInt32(Console.ReadLine());
            doctors = new Doctors[count];
        }


        public void CreateDoctors()
        {
            try
            {
                doctors[size] = new Doctors().CreateDoctor();
                throw new ArrayIndexOutOfBoundException();
                size++;
            }
            catch (ArrayIndexOutOfBoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //public override string ToString()
            //{
            //    Doctors doctors = new Doctors();
            //    return $"{doctors.Id}\n {doctors.Name} \n {doctors.Gender}\n {doctors.Specialization}";
            //}
        }
        public void ShowAllDoctors()
        {
            for(int i = 0; i < size; i++)
            {
                Console.WriteLine($"Doctor Id: {doctors[i].Id}");
                Console.WriteLine($"Doctor Name: {doctors[i].Name}");
                Console.WriteLine($"Doctor Specialization: {doctors[i].Specialization}");
                Console.WriteLine($"Doctor Gender: {doctors[i].Gender}");
                Console.WriteLine("--------------------------------");

            }
        }
    }
}
