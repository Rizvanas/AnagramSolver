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
using System.Diagnostics;
using Core.DTO;
using Microsoft.AspNetCore.Http;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {

        private readonly ISqlWordRepository _sqlWordRepository;
        private readonly IAppConfig _appConfig;
        private readonly IUserLogRepository _userLogRepository;

        public AnagramSolver(ISqlWordRepository sqlWordRepository, IAppConfig appConfig, IUserLogRepository userLogRepository)
        {
            _sqlWordRepository = sqlWordRepository;
            _appConfig = appConfig;
            _userLogRepository = userLogRepository;
        }

        public List<Word> GetAnagrams(string myWords, string IpAdress)
        {
            var stopWatch = new Stopwatch();
            var timeElapsed = 0L;

            stopWatch.Start();
            if (myWords == null)
                throw new ArgumentNullException("myWords cannot be null");

            var anagrams = _sqlWordRepository.GetCachedAnagrams(myWords);
            if (anagrams.Count() != 0)
            {
                stopWatch.Stop();
                timeElapsed = stopWatch.ElapsedMilliseconds;
                LogUserInfo(myWords, IpAdress, timeElapsed);

                return anagrams;
            }

            var resultCount = 1000;
            var words = _sqlWordRepository.SearchWords(myWords).ToList();

            anagrams = FindAnagrams(words, myWords, new List<List<Word>>())
                .Take(resultCount)
                .Select(a => new Word { Text = String.Join(' ', a.Select(t => t.Text)) })
                .Where(a => a.Text != myWords)
                .ToList();

            _sqlWordRepository.AddCachedWord(new Word { Text = myWords }, anagrams);

            stopWatch.Stop();
            timeElapsed = stopWatch.ElapsedMilliseconds;
            LogUserInfo(myWords, IpAdress, timeElapsed);

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

        private void LogUserInfo(string phrase, string ip, long searchTime)
        {
            _userLogRepository.AddUserLog(new UserLog
            {
                SearchPhrase = phrase,
                SearchTime = searchTime,
                UserIP = ip
            });
        }
    }
}
