using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaipApp
{
    public class AnagramGeneratorHandler
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IPrinter _printer;
        private readonly List<List<IWordLoader>> _words;
        private readonly IWordRepository _wordRepository;
        private readonly IWordLoader _wordLoader;

        public AnagramGeneratorHandler
            (
                IAnagramSolver anagramSolver, 
                IWordRepository wordRepository, 
                IWordLoader wordLoader,
                IPrinter printer
            )
        {
            _anagramSolver = anagramSolver;
            _printer = printer;
            _words = wordLoader.Load("..\AnagramGenerator\zodynas.txt");
        }
        public bool run(bool run)
        {
            while (run)
            {
                Console.WriteLine("Please enter > 0 and < 11 words");
                words = Console.ReadLine();
                var wordCount = words.Split(' ').Length;

                if (wordCount >= 1 && wordCount <= 10)
                {
                    var anagrams = _anagramSolver.GetAnagrams(words);
                }

                Console.WriteLine("Press [esc] if you want to exit application.");
                run = Console.ReadKey().Key != ConsoleKey.Escape;
                Console.Clear();
            }


            return run;
        }
    }
}
