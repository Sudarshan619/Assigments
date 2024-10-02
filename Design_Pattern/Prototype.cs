using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design_Pattern
{
    internal class Prototype
    {
        public abstract class Animal
        {
            public abstract Animal Clone();
        }

        public class Sheep : Animal
        {
            public string Name { get; set; }
            public string Breed { get; set; }

            public override Animal Clone()
            {
                return (Animal)this.MemberwiseClone();
            }
        }  

    }
}
