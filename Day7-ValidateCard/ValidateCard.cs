using System;

namespace Day7_ValidateCard
{
    internal class ValidateCard : IValidateCard
    {
        public void isValid(char[] cardIdArray)
        {
            // check the length and that all characters are digits or else throw exception
            if (cardIdArray.Length != 12 || !IsAllDigits(cardIdArray))
            {
                throw new InvalidLengthException();
            }

            ReverseNumber(cardIdArray);
            MultiplyAtEven(cardIdArray);
            is2Digit(cardIdArray);

            if (SumOfAll(cardIdArray) % 10 == 0)
            {
                Console.WriteLine("It's a valid card.");
            }
            else
            {
                Console.WriteLine("Invalid card.");
            }
        }

        private bool IsAllDigits(char[] cardIdArray)
        {
            foreach (char c in cardIdArray)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public void is2Digit(char[] cardIdArray)
        {
            for (int i = 0; i < cardIdArray.Length; i++)
            {
                int temp = int.Parse(cardIdArray[i].ToString());

                if (temp >= 10) // Check if it is a 2-digit number
                {
                    int num1 = temp / 10;
                    int num2 = temp % 10;
                    cardIdArray[i] = (char)((num1 + num2) + '0'); // Convert back to char
                }
            }
        }

        public void MultiplyAtEven(char[] cardIdArray)
        {
            for (int i = 0; i < cardIdArray.Length; i++)
            {
                if (i % 2 == 0) // Check if the index is even
                {
                    int temp = int.Parse(cardIdArray[i].ToString());
                    temp *= 2;

                    // Handle conversion back to char
                    if (temp >= 10)
                    {
                        cardIdArray[i] = (char)((temp / 10 + temp % 10) + '0');
                    }
                    else
                    {
                        cardIdArray[i] = (char)(temp + '0');
                    }
                }
            }
        }

        public void ReverseNumber(char[] cardIdArray)
        {
            int i = 0;
            int j = cardIdArray.Length - 1;

            while (i < j)
            {
                char temp = cardIdArray[i];
                cardIdArray[i] = cardIdArray[j];
                cardIdArray[j] = temp;
                i++;
                j--;
            }
        }

        public int SumOfAll(char[] cardIdArray)
        {
            int sum = 0;

            foreach (char num in cardIdArray)
            {
                sum += int.Parse(num.ToString());
            }
            Console.WriteLine(sum);

            return sum;
        }
    }
}
