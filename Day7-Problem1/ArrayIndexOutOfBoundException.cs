using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_Problem1
{
    internal class ArrayIndexOutOfBoundException:Exception
    {
        public string message { get; set; }

        public ArrayIndexOutOfBoundException() {
            message = "Array index out of bound not suffienct space";
        }

        public override string Message { get { return message; } }

    }
}
