using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Day7_PrimeNumbers
{
    internal class CheckPrime:ICheckPrime
    {
        
        public bool isPrime(int num) { 

            if (num < 2) {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    return false; ;
                }
            }
            return true;
        }
    }
}
