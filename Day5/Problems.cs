using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    internal class Problems
    {
        public double Average()
        {
            int[] arr = new int[10];
            int sum = 0;
            for(int i= 0; i < 10; i++)
            {
                arr[i]= Convert.ToInt32(Console.ReadLine());
            }
            for(int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum/arr.Length;
        }

        public void FindPrime()
        {
            Console.WriteLine("Enter min:");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter max:");
            int max = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Prime number between {min} and {max} are");
            for (int i = min; i < max; i++)
            {

                if (isPrime(i))
                {   
                    Console.Write( i + " ");
                }
            }

        }

        public bool isPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void DivisibleBy3()
        {
            int num = 0;
            while (num != -1)
            {
                Console.WriteLine("Enter the number");
                num = Convert.ToInt32(Console.ReadLine());
                if (num%10 == 3 || num%3 == 0)
                {
                    Console.WriteLine($"Number {num} divisible by 3");
                }
            }
            
        }

        public string ToWords()
        {
            int num = Convert.ToInt32(Console.ReadLine());
            string[] ZeroToNinteen = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                  "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen",
                                  "Eighteen", "Nineteen" };
            string[] TwentyToNinety = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string Words = "";
            if (num == 0)
            {
                return "Zero";
            }
            if(num < 0)
            {
                num = Math.Abs(num);
                return $"Minus {num}";
            }
            if(num/100 > 0)
            {
                Words += ZeroToNinteen[num/100] + " hundred";
                num%=100;
            }
            if (num > 0) { 
                if(Words != "")
                {
                    Words += " and ";
                }
                if (num < 20)
                {
                    Words += ZeroToNinteen[num];
                }
                else
                {
                    Words += TwentyToNinety[num / 10];
                    if(num % 10 > 0)
                    {
                        Words += "-" + ZeroToNinteen[num % 10];
                    }
                }
            }
            return Words;
        }
    }
}
