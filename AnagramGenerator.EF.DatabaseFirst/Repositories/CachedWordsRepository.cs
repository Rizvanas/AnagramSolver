using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class CachedWordsRepository : ICachedWordsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public CachedWordsRepository (WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public IEnumerable<CachedWordEntity> GetCachedWords()
        {
            return _wordsDBContext.CachedWords;
        }

        public IEnumerable<CachedWordEntity> GetCachedWords(PhraseEntity phrase)
        {
            return _wordsDBContext.CachedWords
                .Where(cw => cw.PhraseId == phrase.Id);
        }

        public IEnumerable<CachedWordEntity> GetCachedWords(AnagramEntity anagram)
        {
            return _wordsDBContext.CachedWords
                .Where(cw => cw.AnagramId == anagram.Id);
        }

        public void AddCachedWord(CachedWordEntity cachedWord)
        {
            _wordsDBContext.CachedWords.Add(cachedWord);
            _wordsDBContext.SaveChanges();
        }

        public void AddCachedWord(PhraseEntity phrase, IEnumerable<AnagramEntity> anagrams)
        {
            foreach(var anagram in anagrams)
            {
                _wordsDBContext.CachedWords.Add(new CachedWordEntity
                {
                    Phrase = phrase,
                    PhraseId = phrase.Id,
                    Anagram = anagram,
                    AnagramId = anagram.Id,
                });
            }
            _wordsDBContext.SaveChanges();
        }
    }
}
