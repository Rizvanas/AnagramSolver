using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Contracts.Repositories;
using Contracts.Extensions;
using Contracts.DTO;

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

        public IList<Anagram> GetAnagrams(string word, string IpAdress)
        {
            var stopWatch = new Stopwatch();
            var timeElapsed = 0L;
            stopWatch.Start();
            if (word == null)
                return new List<AnagramEntity>();

            var phrase = _phrasesRepository.GetPhrase(word);
            if (phrase == null)
                phrase = new PhraseEntity { Phrase = word };

            var anagrams = _anagramsRepository.GetAnagrams(phrase);
            if (anagrams.Count() != 0)
            {
                stopWatch.Stop();
                timeElapsed = stopWatch.ElapsedMilliseconds;
                LogUserInfo(phrase, IpAdress, Convert.ToInt32(timeElapsed));

                return anagrams;
            }

            var resultCount = 1000;
            var words = _wordRepository.GetSearchWords(phrase).ToList();

            anagrams = FindAnagrams(words, phrase.Phrase, new List<List<AnagramEntity>>())
                .Take(resultCount)
                .Select(a => new AnagramEntity { Anagram = String.Join(' ', a.Select(t => t.Anagram)) })
                .Where(a => a.Anagram.Replace(" ", "").ToLower() 
                != phrase.Phrase.Replace(" ", "").ToLower())
                .ToList();

            _cachedWordsRepository.AddCachedWord(phrase, anagrams);

            stopWatch.Stop();
            timeElapsed = stopWatch.ElapsedMilliseconds;
            LogUserInfo(phrase, IpAdress, Convert.ToInt32(timeElapsed));

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
                tempSearchWord = tempSearchWord.GetSearchWord(word.Word);

                if (prevSearchWord != tempSearchWord)
                    currentAnagram.Add(new AnagramEntity { Anagram = word.Word });

                if (tempSearchWord.Length == 0 
                    || word.Word.Length > tempSearchWord.Length)
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

        private void LogUserInfo(PhraseEntity phrase, string ip, int searchTime)
        {
            _userLogsRepository.AddUserLog(new UserLogEntity
            {
                UserIp = ip,
                SearchPhrase = phrase,
                SearchPhraseId = phrase.Id,
                SearchTime = searchTime
            });
        }
    }
}
