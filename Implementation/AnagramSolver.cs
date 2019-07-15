using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Contracts.Entities;
using Contracts.Repositories;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {

        private readonly IWordsRepository _wordRepository;
        private readonly IAnagramsRepository _anagramsRepository;
        private readonly IPhrasesRepository _phrasesRepository;
        private readonly IUserLogsRepository _userLogsRepository;
        private readonly ICachedWordsRepository _cachedWordsRepository;

        private readonly IAppConfig _appConfig;

        public AnagramSolver(IWordsRepository wordsRepository,
            IAnagramsRepository anagramsRepository,
            IPhrasesRepository phrasesRepository,
            IUserLogsRepository userLogsRepository,
            ICachedWordsRepository cachedWordsRepository,
            IAppConfig appConfig)
        {
            _wordRepository = wordsRepository;
            _anagramsRepository = anagramsRepository;
            _phrasesRepository = phrasesRepository;
            _userLogsRepository = userLogsRepository;
            _cachedWordsRepository = cachedWordsRepository;
            _appConfig = appConfig;
        }

        public IEnumerable<AnagramEntity> GetAnagrams(string myWords, string IpAdress)
        {
            var stopWatch = new Stopwatch();
            var timeElapsed = 0L;

            stopWatch.Start();
            if (myWords == null)
                throw new ArgumentNullException("myWords cannot be null");

            var phrase = _phrasesRepository.GetPhrase(myWords);
            var anagrams = _anagramsRepository.GetAnagrams(phrase);

            if (anagrams.Count() != 0)
            {
                stopWatch.Stop();
                timeElapsed = stopWatch.ElapsedMilliseconds;
                LogUserInfo(myWords, IpAdress, timeElapsed);

                return anagrams;
            }

            var resultCount = 1000;
            var words = _wordRepository.GetSearchWords(phrase).ToList();

            anagrams = FindAnagrams(words, myWords, new List<List<AnagramEntity>>())
                .Take(resultCount)
                .Select(a => new AnagramEntity { Anagram = String.Join(' ', a.Select(t => t.Anagram)) })
                .Where(a => a.Anagram.Replace(" ", "").ToLower() 
                != phrase.Phrase.Replace(" ", "").ToLower())
                .ToList();

            _cachedWordsRepository.AddCachedWord(new CachedWordEntity {Phrase = phrase });

            stopWatch.Stop();
            timeElapsed = stopWatch.ElapsedMilliseconds;
            LogUserInfo(myWords, IpAdress, timeElapsed);

            return anagrams;
        }

        private List<List<AnagramEntity>> FindAnagrams(List<WordEntity> words, string searchWord, List<List<AnagramEntity>> results)
        {
            var currentAnagram = new List<AnagramEntity>();
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
