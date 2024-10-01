using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    public class Savings : IProduct
    {
        static List<Account> accounts = new List<Account>() {
             new Account(){Id=1,Name="Sudarshan",Balance=0,AccountNumber=123456789},
             new Account(){Id=2,Name="Adi",Balance=1000,AccountNumber=2332354544}
        }
            ;

        void IProduct.CreateAccount()
        {
            Account account = new Account();
            
            account.Id = accounts.Max(e=>e.Id)+1;
            Console.WriteLine("Enter name");
            account.Name = Console.ReadLine();
            Console.WriteLine("Account number");
            account.AccountNumber = Convert.ToInt64(Console.ReadLine());
            accounts.Add(account);

        }

        void IProduct.MakeDeposit(float amount, int accountNumber)
        {
            var account = accounts.FirstOrDefault(e => e.Id == accountNumber);
            account.Balance += amount;

        }

        void IProduct.ShowBalance(int accountNumber)
        {
            var account = accounts.FirstOrDefault(e => e.Id== accountNumber);
            Console.WriteLine(account.Balance);

        }

        void IProduct.Withdraw(float amount, int accountNumber)
        {
            var account = accounts.FirstOrDefault(e => e.Id == accountNumber);
            account.Balance -= amount;
            Console.WriteLine(account.Balance);

        }
    }
}
