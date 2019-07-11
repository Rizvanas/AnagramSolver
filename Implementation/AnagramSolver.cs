using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Extensions;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;
using Core.Domain;
using System.Reflection;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {

        private readonly ISqlWordRepository _sqlWordRepository;
        private readonly IAppConfig _appConfig;

        public AnagramSolver(ISqlWordRepository sqlWordRepository, IAppConfig appConfig)
        {
            _sqlWordRepository = sqlWordRepository;
            _appConfig = appConfig;
        }

        public List<string> GetAnagrams(string myWords)
        {
            if (myWords == null)
                throw new ArgumentNullException("myWords cannot be null");

            var anagrams = _sqlWordRepository.GetCachedAnagrams(myWords);
            if (anagrams.Count() != 0)
                return anagrams;

            var resultCount = 1000;
            myWords = myWords.Replace(" ", "");
            var words = _sqlWordRepository.SearchWords(myWords).ToList();

            anagrams = FindAnagrams(words, myWords, new List<List<Word>>())
                .Take(resultCount)
                .Select(a => String.Join(' ', a.Select(t => t.Text)))
                .ToList();

            _sqlWordRepository.AddCachedWord(new Word { Text = myWords }, anagrams);
            return anagrams;
        }

        private List<List<Word>> FindAnagrams(List<Word> words, string searchWord, List<List<Word>> results)
        {
            var currentAnagram = new List<Word>();
            string tempSearchWord = searchWord;
            string prevSearchWord = null;

            foreach(var word in words)
            {
                prevSearchWord = tempSearchWord;
                tempSearchWord = tempSearchWord.GetSearchWord(word.Text);

                if (prevSearchWord != tempSearchWord)
                    currentAnagram.Add(word);

                if (tempSearchWord.Length == 0 
                    || word.Text.Length > tempSearchWord.Length)
                    break;
            }

            if (tempSearchWord.Length == 0)
                results.Add(currentAnagram);

            if (words.Count > 0 && searchWord.Length != 0)
            {
                words.RemoveAt(0);
                FindAnagrams(words, searchWord, results);
            }

            return results;
        }
    }
}
