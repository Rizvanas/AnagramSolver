using Core.Interfaces;
using Core.Printables;
using Implementation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AnagramGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine("Iveskite dalykus...");
            var words = Console.ReadLine();
            var anagramGetter = new AnagramWebAppClient(new HttpClient());
            var anagrams = anagramGetter.GetAnagramsAsync(words).Result;

            var printer = new ConsoleWordPrinter();
            printer.Print(new List<IPrintable> { new Anagrams(anagrams) });
        }
    }
}
