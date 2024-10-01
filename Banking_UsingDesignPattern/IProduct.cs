using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal interface IProduct
    {
        void CreateAccount();
        void MakeDeposit(float amount,int account);

        void Withdraw(float amount,int account);

        void ShowBalance(int account);
    }
}
