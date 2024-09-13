using Day7_ClassWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_ClassWork
{
    public class PremiumCustomer : Customer
    {
        public string Membership { get; set; }
        public PremiumCustomer()
        {
            Membership = "Gold";
        }
        public override void SetCustomerDetails()
        {
            try {
                base.SetCustomerDetails();
                Console.WriteLine("Enter the membership type:");
                Membership = Console.ReadLine() ?? "Gold";
            }
            catch (Exception e)
            {
                
                    Console.WriteLine("Error occured");
              
               
                
            }
           
        }
    }
}
