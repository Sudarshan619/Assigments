using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Decorator
    {
        public interface ICar
        {
            string Make();
        }

        public class BasicCar : ICar
        {
            public string Make()
            {
                return "Basic Car";
            }
        }

        public class CarDecorator : ICar
        {
            protected ICar _car;

            public CarDecorator(ICar car)
            {
                _car = car;
            }

            public virtual string Make()
            {
                return _car.Make();
            }
        }

        public class SportsCar : CarDecorator
        {
            public SportsCar(ICar car) : base(car) { }

            public override string Make()
            {
                return $"{base.Make()} + Sports Features";
            }
        }

      
    }
}
