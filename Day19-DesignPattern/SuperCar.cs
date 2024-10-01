using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19_DesignPattern
{
    internal class SuperCar : ICar
    {
        public void GetDetails()
        {
            Console.WriteLine("Details from super car");
        }
    }
}
