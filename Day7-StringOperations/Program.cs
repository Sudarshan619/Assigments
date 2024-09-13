namespace Day7_StringOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWordOperations wordOperations = new WordOperation();
            //IWordOperations leastRepeatingVowels = new WordWithLeastRepeatingVowels();

            try
            {
                Console.WriteLine("Enter a comma-separated list of words for longest and shortest word:");
                string input1 = Console.ReadLine();
                wordOperations.FindLongestAndShortest(input1);

                Console.WriteLine("\nEnter a comma-separated list of words for least repeating vowels:");
                string input2 = Console.ReadLine();
                wordOperations.FindWordWithLeastRepeatingVowels(input2);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
