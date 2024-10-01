using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_DeignPattern
{
    internal class CarModel
    {
        List<string> carsParts =  new List<string>();
        public CarModel() { }

        public void Add(string data)
        {
            carsParts.Add(data);
        }
        public void GetCarsParts() {
            foreach (string part in carsParts) { 
              Console.WriteLine(part);
            }
            
        }

    }
}
