using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Contracts.Repositories;
using Contracts.Extensions;
using Contracts.DTO;
using Contracts.Services;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {

        private readonly IWordsService _wordsService;
        private readonly IPhrasesService _phrasesService;
        private readonly IUserLogsService _userLogsService;
        private readonly IUsersService _usersService;
        private readonly ICachedWordsService _cachedWordsService;
        private readonly IAppConfig _appConfig;

        public AnagramSolver(IWordsService wordsService,
            IPhrasesService phrasesService,
            IUserLogsService userLogsService,
            IUsersService usersService,
            ICachedWordsService cachedWordsService,
            IAppConfig appConfig)
        {
            _wordsService = wordsService;
            _phrasesService = phrasesService;
            _userLogsService = userLogsService;
            _usersService = usersService;
            _cachedWordsService = cachedWordsService;
            _appConfig = appConfig;
        }

        public IList<Anagram> GetAnagrams(string word, User user)
        {
            var stopWatch = new Stopwatch();
            var timeElapsed = 0L;
            stopWatch.Start();
            if (word == null)
                return new List<Anagram>();

            var phrase = _phrasesService.GetPhrase(word);
            if (phrase == null)
            {
                _phrasesService.AddPhrase(word);
                phrase = _phrasesService.GetPhrase(word);
            }

            var anagrams = _cachedWordsService.GetAnagrams(phrase).ToList();
            if (anagrams.Count() != 0)
            {
                stopWatch.Stop();
                timeElapsed = stopWatch.ElapsedMilliseconds;
                _userLogsService.LogUserInfo(phrase, user, anagrams, Convert.ToInt32(timeElapsed));
                _usersService.UpdateUserSearchesCount(user.Id, user.SearchesLeft + 1);

                return anagrams;
            }

            var resultCount = 1000;
            var words = _wordsService.GetWordsForSearch(word);

            anagrams = FindAnagrams(words, phrase.Text, new List<List<Anagram>>())
                .Take(resultCount)
                .Select(a => new Anagram { Text = String.Join(' ', a.Select(t => t.Text)) })
                .Where(a => a.Text.Replace(" ", "").ToLower() 
                != phrase.Text.Replace(" ", "").ToLower())
                .ToList();

            _cachedWordsService.AddCachedWord(phrase, anagrams);
            anagrams = _cachedWordsService.GetAnagrams(phrase).ToList();

            stopWatch.Stop();
            timeElapsed = stopWatch.ElapsedMilliseconds;
            _userLogsService.LogUserInfo(phrase, user, anagrams, Convert.ToInt32(timeElapsed));
            _usersService.UpdateUserSearchesCount(user.Id, user.SearchesLeft - 1);

            return anagrams;
        }

        private List<List<Anagram>> FindAnagrams(IList<Word> words, string searchWord, List<List<Anagram>> results)
        {
            var currentAnagram = new List<Anagram>();
            string tempSearchWord = searchWord;
            string prevSearchWord = null;

            foreach(var word in words)
            {
                prevSearchWord = tempSearchWord;
                tempSearchWord = tempSearchWord.GetSearchWord(word.Text);

                if (prevSearchWord != tempSearchWord)
                    currentAnagram.Add(new Anagram { Text = word.Text });

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
