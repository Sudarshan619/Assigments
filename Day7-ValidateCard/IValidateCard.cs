using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_ValidateCard
{
    public interface IValidateCard
    {
        public void ReverseNumber(char[] cardIdArray);

        public void MultiplyAtEven(char[] cardIdArray);

        public void is2Digit(char[] cardIdArray);

        public int SumOfAll(char[] cardIdArray);

        public void isValid(char[] cardIdArray);
    }
}
