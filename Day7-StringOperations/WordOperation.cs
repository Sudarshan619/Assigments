using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7_StringOperations
{
    internal class WordOperation:IWordOperations
    {
        public void FindLongestAndShortest(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentException("Input string cannot be null or empty.");
                 }

                      string[] words = input.Split(',');

                if (words.Length == 0)
                {
                    throw new ArgumentException("No words found in input string.");
                 }

                    string longest = string.Empty;
                    string shortest = words[0];

                foreach (var word in words)
                {
                    //remove leading white space
                    string trimmedWord = word.Trim();
                    if (trimmedWord.Length > longest.Length)
                    {
                        longest = trimmedWord;
                    }
                
                    if (trimmedWord.Length < shortest.Length)
                    {
                        shortest = trimmedWord;
                    }
                }

                 Console.WriteLine($"Longest word: {longest}");
                 Console.WriteLine($"Shortest word: {shortest}");
              }
              catch (Exception ex)
              {
                  Console.WriteLine($"Error: {ex.Message}");
              }
          }

        public void FindWordWithLeastRepeatingVowels(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new ArgumentException("Input string cannot be null or empty.");
                }

                string[] words = input.ToLower().Split(',');

                if (words.Length == 0)
                {
                    throw new ArgumentException("No words found in input string.");
                }
                
                string vowels = "aeiou";
                int leastRepeatingCount = int.MaxValue;
                //Creating list because the size is not known so to add dynamically
                var resultWords = new List<string>();

                foreach (var word in words)
                {
                    var trimmedWord = word.Trim();
                    //separates within the same word and creates a group of related letter, checks for vowels from the vowel
                    //string and return the filtered letters and get the count of all the letter and convert to array
                    var vowelCounts = trimmedWord.GroupBy(c => c)
                                                 .Where(c => vowels.Contains(c.Key))
                                                 .Select(c => c.Count())
                                                 .ToArray();

                    int repeatCount = vowelCounts.Where(c => c > 1).Sum();

                    if (repeatCount < leastRepeatingCount)
                    {
                        leastRepeatingCount = repeatCount;
                        resultWords.Clear();
                        resultWords.Add(trimmedWord);
                    }
                    else if (repeatCount == leastRepeatingCount)
                    {
                        resultWords.Add(trimmedWord);
                    }
                }

                Console.WriteLine($"Words with least repeating vowels: {string.Join(", ", resultWords)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
  }
