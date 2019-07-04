using Implementation;
using Microsoft.Extensions.Configuration;

using System;
using System.IO;
using System.Text;

namespace MaipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;

            var wordRepository = new WordRepository(new TxtWordLoader());
            var anagramSolver = new AnagramSolver(wordRepository);

            string words = null;
            bool run = true;
            while(run)
            {
                Console.WriteLine("Please enter > 0 and < 11 words");
                words = Console.ReadLine();
                var wordCount = words.Split(' ').Length;

                if (wordCount >= 1 && wordCount <= 10)
                {
                    var anagrams = anagramSolver.GetAnagrams(words);
                }

                Console.WriteLine("Press [esc] if you want to exit application.");
                run = Console.ReadKey().Key != ConsoleKey.Escape;
                Console.Clear();
            }
        }
    }
}
