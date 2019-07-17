using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class CachedWordsRepository : ICachedWordsRepository
    {
        public void AddCachedWord(CachedWordEntity cachedWord)
        {
            throw new NotImplementedException();
        }

        public void AddCachedWord(PhraseEntity phrase, IEnumerable<AnagramEntity> anagrams)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CachedWordEntity> GetCachedWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CachedWordEntity> GetCachedWords(PhraseEntity phrase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CachedWordEntity> GetCachedWords(AnagramEntity anagram)
        {
            throw new NotImplementedException();
        }
    }
}
