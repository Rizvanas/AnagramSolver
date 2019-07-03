using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Extensions;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        private readonly NameValueCollection _appSettings;

        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            _appSettings = ConfigurationManager.GetSection("ApplicationSettings")
                as NameValueCollection;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            var resultCount = Convert.ToInt32(_appSettings["ResultCount"]);
            myWords = Regex.Replace(myWords, @"\s+", "");
            var words = GetWordsForAnagrams(myWords);
            var searchWord = myWords;
            var anagrams = FindAnagrams(words.ToList(), searchWord, new List<string>());

            return anagrams.Take(resultCount).ToList();
               
        }

        private IEnumerable<string> GetWordsForAnagrams(string myWords)
        {
            var minWordLen = Convert.ToInt32(_appSettings["MinWordLength"]);
            var searchWord = myWords.GetSearchWord(null);

            return _wordRepository
                    .GetWords()
                    .Where(w => myWords.GetSearchWord(w.Key) != searchWord
                    && w.Key.Count(c => !Char.IsWhiteSpace(c)) <= searchWord.Length
                    && w.Key.Length >= minWordLen
                    && w.Key.Length >= 1)
                    .OrderByDescending(w => w.Key.Length)
                    .Select(w => w.Key);
        }

        private IList<string> FindAnagrams(List<string> words, string searchWord, List<string> results)
        {
            var currentAnagram = new StringBuilder();
            string tempSearchWord = searchWord;
            string prevSearchWord = null;

            foreach(var word in words)
            {
                prevSearchWord = tempSearchWord;
                tempSearchWord = tempSearchWord.GetSearchWord(word);

                if (prevSearchWord != tempSearchWord)
                    currentAnagram.Append(word).Append(" ");

                if (tempSearchWord.Length == 0 
                    || word.Length > tempSearchWord.Length)
                    break;
            }

            if (tempSearchWord.Length == 0)
                results.Add(currentAnagram.ToString());

            if (words.Count > 0 && searchWord.Length != 0)
            {
                words.RemoveAt(0);
                FindAnagrams(words, searchWord, results);
            }

            return results;
        }
    }
}
