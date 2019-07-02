using Implementation;
using Interfaces;
using System;

namespace MaipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var wordRepository = new WordRepository(new TxtWordLoader());
            var anagramSolver = new AnagramSolver(wordRepository);
            anagramSolver.GetAnagrams("asilas    bananas");
        }
    }
}
