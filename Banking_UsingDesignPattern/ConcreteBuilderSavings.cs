using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal class ConcreteBuilderSavings : IBuilder
    {
        void IBuilder.CreateAccount(BankCreator creator)
        {
            creator.CreateAccount();
        }

        void IBuilder.EnterAccount()
        {
            throw new NotImplementedException();
        }
    }
}
