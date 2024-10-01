using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal class ConcreteSavings : BankCreator
    {
        public override IProduct FactoryMethod()
        {
            return new Savings();
        }
    }
}
