using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class InvalidInputDetailException:Exception
    {
        public string message;
        public InvalidInputDetailException() {
            message = "invalid input please try again ";
        }
        public override string Message{
            get{
                return message; 
            }
        }    
        //public override string Message => message;

    }
}
