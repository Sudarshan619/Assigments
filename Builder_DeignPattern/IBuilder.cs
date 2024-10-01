using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_DeignPattern
{
    internal interface IBuilder
    {
        void CreateBody();
        void AddEngine();
        void AddInterior();
        void AddColour();
        public void GetCarDetails();
    }
}
