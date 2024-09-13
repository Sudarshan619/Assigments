using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_ClassWork
{
    public class Validation : IValidation

    {
       
        Customer customer;

        public int age { get; set; }
        public Validation() {
          customer =  new PremiumCustomer();
        }


        private void GetCustomer() {
            customer.SetCustomerDetails();
        }
        
        private bool ValidateCustomer()
        {
            var diff = DateTime.Now - customer.DOB;
            age = diff.Days / 365;
            if (age<18)
            {
                return false;
            }

            return true;
        }

        public void GetCustomerAndPrintResult()

        {
            GetCustomer();
            if (ValidateCustomer())
            {
                Console.WriteLine($"{customer.Name} your currrent age is {age} You are eligible to vote");
            }
            else
            {
                Console.WriteLine($"{customer.Name}  your age is {age} Not eligible to vote");
            }
            
        }
    }
}
