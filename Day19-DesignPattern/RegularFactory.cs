using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19_DesignPattern
{
    internal class RegularFactory : IFactory
    {
        public IBike CreateBike()
        {
            return new SuperBike();
        }

        public ICar CreateCar()
        {
            return new SuperCar();
        }
    }
}
