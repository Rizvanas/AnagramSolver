using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Extensions;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            var anagrams = new List<string>();
            var separatedWords = myWords.Split(null);

            var words1 = _wordRepository.GetWords().Where(w => separatedWords.Contains(w.Key));
            var wordCombos = _wordRepository


            return anagrams;
        }

        private Dictionary<string, string> GetWordCombos(Dictionary<string, string> words, int depth)
        {
            if (depth != 1)
                return GetWordCombos(words, depth - 1);

            return words.Concat(_wordRepository.GetWords().ToList());
        }
    }
}
