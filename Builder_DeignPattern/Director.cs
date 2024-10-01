using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_DeignPattern
{
    internal class Director
    {
        private IBuilder _builder;

        public IBuilder builder
        { 
            set { _builder = value; }
        }

        public Director() { }

        public void BuildSuperCar()
        {
            _builder.CreateBody();
            _builder.AddColour();
            _builder.AddEngine();
            _builder.AddInterior();
            _builder.GetCarDetails();
        }

        public void BuildNormalCar()
        {
            _builder.CreateBody();
            _builder.AddColour();
            _builder.AddEngine();
            _builder.GetCarDetails();
        }
    }
}
