﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6_ClassWork1
{
    public class NRIAccount : IBankAccount
    {
        private double _balance;
        private const double MinBalance = 10000;
        private const double TransactionCharge = 0.02; // 2%

        public NRIAccount(double initialBalance)
        {
            if(initialBalance >= MinBalance)
            {
                _balance = initialBalance;
            }


            else
            {
                var remainingAmount = (MinBalance - initialBalance);
                Console.WriteLine($"Minimum 10000 is required , Do you want to add {remainingAmount},yes/no");
                var choice = Console.ReadLine().ToLower();
                if (choice == "yes")
                {
                    Deposit(remainingAmount + initialBalance);
                }

            }
        }

        public void Deposit(double amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited Rs. {amount}, new balance: Rs. {_balance}");
        }

        public void Withdraw(double amount)
        {
            double charge = amount * TransactionCharge;
            //check if the amount is withdrawable after charges and min balance
            if (_balance - amount - charge >= MinBalance)
            {
                _balance -= (amount + charge);
                Console.WriteLine($"Withdrew Rs. {amount}, transaction charge: Rs. {charge}, new balance: Rs. {_balance}");
            }
             //if amount exceeds the minwithdrawable limit it will show the maximum withdrawble amount if yes you can continue
            else
            {
                var withdrawable = _balance - charge - MinBalance;
                if (withdrawable <= 0)
                {
                    Console.WriteLine("Not enough balance ");
                }
                else
                {
                    Console.WriteLine($"Insufficient balance. You must maintain a minimum balance of Rs. 10000.\n Current balance:{_balance} You can withdraw maximum amount of rs {withdrawable} \n,Do you wish to withdraw ,yes/no");
                    var choice = Console.ReadLine().ToLower();
                    if (choice == "yes")
                    {
                      
                        Withdraw(withdrawable);

                    }
                }
                
            }
        }

        public void ShowBalance()
        {
            Console.WriteLine($"NRI Account Balance: Rs. {_balance}");
        }
    }

}
