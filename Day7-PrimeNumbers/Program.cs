namespace Day7_PrimeNumbers
{
    internal class Program
    {
        ICheckPrime checkPrime = new CheckPrime();

        public int number = 0;
        bool isPrime(int number) {
           return checkPrime.isPrime(number);
        }
        public void GetNumber()
        {
            Console.WriteLine("Enter the number");
            number = Convert.ToInt32(Console.ReadLine());

        }

        static void Main(string[] args)
        {
            Program program = new Program();

            do
            {
                program.GetNumber();

                if (program.isPrime(program.number)) {
                    Console.WriteLine($"{program.number} a prime");
                }
                else
                {

                    Console.WriteLine($"{program.number} a not  prime");
                }
                

            } while (program.number != 0);

            
        }
    }
}
