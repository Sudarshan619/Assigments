using Day27_Webapi_EF.Interface;

namespace Day27_Webapi_EF
{
    public class Calculate : ICalculate
    {
        public int Add(int value1, int value2)
        {
            return value1 + value2;
        }

        public int Multiply(int value1, int value2)
        {
            return value1 * value2;
        }
    }
}
