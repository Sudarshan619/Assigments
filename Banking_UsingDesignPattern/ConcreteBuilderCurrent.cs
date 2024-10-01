using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal class ConcreteBuilderCurrent : IBuilder
    {
        BankCreator creator = new ConcreteCurrent();
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
