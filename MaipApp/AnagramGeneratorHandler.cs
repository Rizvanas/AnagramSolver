using Core.Domain;
using Core.Interfaces;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Printables;
using System.Linq;

namespace MaipApp
{
    public class AnagramGeneratorHandler
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IPrinter _printer;
        private readonly IWordRepository _wordRepository;

        public AnagramGeneratorHandler(IAnagramSolver anagramSolver, IPrinter printer)
        {
            _anagramSolver = anagramSolver;
            _printer = printer;
        }

        public bool Run(bool continueRunning)
        {
            if (continueRunning)
            {
                Console.WriteLine("Please enter > 0 and < 11 words");
                var inputWords = Console.ReadLine();
                var wordCount = inputWords.Split(' ').Length;

                if (wordCount >= 1 && wordCount <= 10)
                {
                    var anagrams = _anagramSolver
                        .GetAnagrams(inputWords)
                        .Select(a => String.Join(' ', a.Select(t => t.Text)))
                        .ToList();
                    var pritableAnagrams = new Anagrams(anagrams);
                    _printer.Print(new List<IPrintable> { new Anagrams(anagrams) });
                }

                Console.Clear();
            }

            return continueRunning;
        }
    }
}
