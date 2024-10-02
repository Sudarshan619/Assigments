using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class FactoryMethod
    {
        public interface IVehicle
        {
            void Drive();
        }

        public class Car : IVehicle
        {
            public void Drive()
            {
                Console.WriteLine("Driving a car.");
            }
        }

        public class Bike : IVehicle
        {
            public void Drive()
            {
                Console.WriteLine("Riding a bike.");
            }
        }

        public class VehicleFactory
        {
            public IVehicle GetVehicle(string vehicleType)
            {
                switch (vehicleType)
                {
                    case "Car": return new Car();
                    case "Bike": return new Bike();
                    default: throw new Exception("Unknown vehicle type.");
                }
            }
        }
 
        

    }
}
