using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork1
{
     //Interface bank account which needs to be implemented separately for each bank 
    internal interface IBankAccount
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void ShowBalance();
    }
}
