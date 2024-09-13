using System;

namespace Day7_ValidateCard
{
    internal class Program
    {
        IValidateCard _validateCard;

        static string CardId { get; set; }
        static char[] CardIdArray;

        
        public Program(IValidateCard validateCard)
        {
            _validateCard = validateCard;
        }

        public void GetCardNumber()
        {
            Console.WriteLine("Enter card number:");
            CardId = Console.ReadLine();
            CardIdArray = new char[CardId.Length];

            for (int i = 0; i < CardId.Length; i++)
            {
                CardIdArray[i] = CardId[i];
            }
        }

        static void Main(string[] args)
        {     
            Program program = new Program(new ValidateCard());
            try
            {
                program.GetCardNumber();
                program._validateCard.isValid(CardIdArray);
            }
            catch (InvalidLengthException ex) { 
               
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
