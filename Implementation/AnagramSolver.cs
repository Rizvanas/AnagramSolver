﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Extensions;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;
using Core.Domain;

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

        public List<List<Word>> GetAnagrams(string myWords)
        {
            var resultCount = Convert.ToInt32(_appSettings["ResultCount"]);
            myWords = Regex.Replace(myWords, @"\s+", "");
            var words = GetWordsForAnagrams(myWords);
            var searchWord = myWords;
            var anagrams = FindAnagrams(words, searchWord, new List<List<Word>>());

            return anagrams.Take(resultCount).ToList();
        }

        private List<Word> GetWordsForAnagrams(string myWords)
        {
            var minWordLen = Convert.ToInt32(_appSettings["MinWordLength"]);
            var searchWord = myWords.GetSearchWord(null);

            return _wordRepository
                    .GetWords()
                    .Where(w => myWords.GetSearchWord(w.Text) != searchWord
                    && w.Text.Count(c => !Char.IsWhiteSpace(c)) <= searchWord.Length
                    && w.Text.Length >= minWordLen
                    && w.Text.Length >= 1)
                    .OrderByDescending(w => w.Text.Length)
                    .ToList();
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