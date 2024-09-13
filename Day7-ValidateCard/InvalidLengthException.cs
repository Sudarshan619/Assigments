using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_ValidateCard
{
    internal class InvalidLengthException:Exception
    {
        public string message { get; set; }

        public InvalidLengthException() {
            message = "Digits greater than 12 or contains other than digits, Please provide correct digits";
        } 

        public override string Message { get { return message; } }
    }
}
