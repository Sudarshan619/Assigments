using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19_DesignPattern
{
    internal interface IFactory
    {
        ICar CreateCar();

        IBike CreateBike();

    }
}
