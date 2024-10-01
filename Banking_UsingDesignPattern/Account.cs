using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_UsingDesignPattern
{
    internal class Account
    {
        public Account() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public float Balance {  get; set; }

        public long AccountNumber {  get; set; }
    }
}
