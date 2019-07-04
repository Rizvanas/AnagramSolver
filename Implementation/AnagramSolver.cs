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
using System.Reflection;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly List<Word> _words;
        private readonly Configuration _appSettings;

        public AnagramSolver(List<Word> words)
        {
            _words = words;
            //_appSettings = ConfigurationManager.GetSection("ApplicationSettings")
            //            as NameValueCollection;
            _appSettings = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
        }

        public List<List<Word>> GetAnagrams(string myWords)
        {
            if (myWords == null)
                throw new ArgumentNullException("myWords cannot be null");

            var resultCount = 1000;
            myWords = Regex.Replace(myWords, @"\s+", "");
            var words = GetWordsForAnagrams(myWords);
            var searchWord = myWords;
            var anagrams = FindAnagrams(words, searchWord, new List<List<Word>>());

            return anagrams.Take(resultCount).ToList();
        }

        private List<Word> GetWordsForAnagrams(string myWords)
        {
            var minWordLen = Convert.ToInt32(2);
            var searchWord = myWords.GetSearchWord(null);

            return _words
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
