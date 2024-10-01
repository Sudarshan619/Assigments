using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19_DesignPattern
{
    internal class SuperBike : IBike
    {
        public void GetDetails()
        {
            Console.WriteLine("Details from super bike");
        }
    }
}
