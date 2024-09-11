using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork1
{
    
        public class SalaryAccount : IBankAccount
    {
            private double _balance;

            public SalaryAccount(double initialBalance)
            {
                _balance = initialBalance;
            }

            public void Deposit(double amount)
            {
                _balance += amount;
                Console.WriteLine($"Deposited Rs. {amount}, new balance: Rs. {_balance}");
            }

            public void Withdraw(double amount)
            {
                if (_balance >= amount)
                {
                    _balance -= amount;
                    Console.WriteLine($"Withdrew Rs. {amount}, no transaction charge, new balance: Rs. {_balance}");
                }
                else
                {
                    Console.WriteLine("Insufficient balance.");
                }
            }

            public void ShowBalance()
            {
                Console.WriteLine($"Salary Account Balance: Rs. {_balance}");
            }
        }

    
}
