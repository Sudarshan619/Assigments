using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Expenses
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public int Amount { get; set; } 
        public double Balance { get; set; }

        public string ExpenseCategory { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime ExpenseCreatedAt { get; set; }



        
        public Expenses() { 
            ExpenseCreatedAt = DateTime.Now;

        } 
        
        public void ShowExpenses()
        {   
            Console.WriteLine($"Id:{Id}\n Name:{Name}\n Amount:{Id}\n Id:{Id}\n Id:{Id}\n");
        }

    }
}
