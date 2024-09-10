using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Employee
    {
        public Employee(int id, string name, string designation, double salary, DateTime dateOfBirth, int totalLeave)
        {
            Id = id;
            Name = name;
            Designation = designation;
            Salary = salary;
            DateOfBirth = dateOfBirth;
            TotalLeave = totalLeave;
            Total = 1000;
            MaxExpense = 100;
        }

        public Employee()

        {
            Total = 1000;
            MaxExpense = 100;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TotalLeave { get; set; }

        public int Total {  get; set; }

        public int MaxExpense { get; set; }
        public void DoWork()
        {
            Console.WriteLine("Employee does his work");
        }
        private double num1;
        public void TakeLeave()
        {
            num1 = 10;
            if (TotalLeave > 0)
            {
                TotalLeave--;
                Console.WriteLine($"{Name} takes leave. Remaining leave - {TotalLeave}");
            }
            else
            {
                Console.WriteLine($"{Name} has no leave balance");
            }
        }
    }


}
