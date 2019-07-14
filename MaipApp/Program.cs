using Implementation;
using Microsoft.Extensions.Configuration;

using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace MaipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new AppConfig();
            Console.InputEncoding = Encoding.Unicode;
            var fileLoader = new FileLoader(configuration);
            var fileWordRepository = new FileWordRepository(fileLoader);
            //var words = wordRepository.GetWords().ToList();
            //var sqlWordRepository = new SqlWordRepository(configuration);

            var words = fileWordRepository.GetWords(null).ToList();
            fileLoader.BulkFillWordsTable(words);
            
            //txtWordLoader.BulkFillWordsTable(wordRepository.GetWords().ToList());
            //var anagramSolver = new AnagramSolver(wordRepository);
            var printer = new ConsoleWordPrinter();
            //var anagramGeneratorHandler = new AnagramGeneratorHandler(anagramSolver, printer);

            bool continueRunning = true;
            //while(anagramGeneratorHandler.Run(continueRunning))
            //{
            //    Console.WriteLine("Press [esc] if you want to exit application.");
            //    continueRunning = Console.ReadKey().Key != ConsoleKey.Escape;
            //    Console.Clear();
            //}
        }
    }
}
