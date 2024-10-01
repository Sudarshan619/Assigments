using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal abstract class BankCreator
    {
        public abstract IProduct FactoryMethod();
       
        public void Deposit(float amount,int accNo)
        {
            var product = FactoryMethod();
            product.MakeDeposit(amount, accNo);

        }

        public void Withdraw(float amount,int accNo)
        {
            var product = FactoryMethod();
            product.Withdraw(amount, accNo);

        }

        public void ShowBalance(int accNo)
        {
            var product = FactoryMethod();
            product.ShowBalance(accNo);

        }

        public void CreateAccount() { 
           var product = FactoryMethod();
            product.CreateAccount();
        }

    }
}
