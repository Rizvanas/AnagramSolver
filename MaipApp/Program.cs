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
            var printer = new ConsoleWordPrinter();
            var anagramGeneratorHandler = new AnagramGeneratorHandler(anagramSolver, printer);

            bool continueRunning = true;
            while(anagramGeneratorHandler.Run(continueRunning))
            {
                Console.WriteLine("Press [esc] if you want to exit application.");
                continueRunning = Console.ReadKey().Key != ConsoleKey.Escape;
            }
        }
    }
}
