using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal interface IBuilder
    {
        void CreateAccount(BankCreator creator);

        void EnterAccount();

    }
}
