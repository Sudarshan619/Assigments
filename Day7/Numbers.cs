using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    public class Numbers
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }

        public void TakeNumbersFromConsole()
        {
            try
            {
                Console.WriteLine("Please enter the first number");
                Number1 = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Please enter the second number");
                Number2 = Convert.ToDouble(Console.ReadLine());
            }

            catch (FormatException e)
            {
                throw new FormatException();

            }
            catch (OverflowException e)
            {
                Console.WriteLine("Overflow exception");
            }
            catch (Exception e)
            {
                Console.WriteLine("An occured");

            }
        }

        



    }
}
