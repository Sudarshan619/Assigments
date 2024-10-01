using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_DeignPattern
{
    internal class CarBuilder:IBuilder
    {
        CarModel model = new CarModel();
        public CarBuilder() { 
            
        }
        public void reset() { 
            model = new CarModel();
        }
        void IBuilder.AddColour()
        {
            model.Add("Adding color to the car");
        }

        void IBuilder.AddEngine()
        {
            model.Add("Adding engine to the car");
        }


        void IBuilder.AddInterior()
        {
            model.Add("Adding interior to the car");
        }

        void IBuilder.CreateBody()
        {
            model.Add("Adding body to the car");
        }
        void IBuilder.GetCarDetails()
        {
            model.GetCarsParts();
            reset();
        }
    }
}
