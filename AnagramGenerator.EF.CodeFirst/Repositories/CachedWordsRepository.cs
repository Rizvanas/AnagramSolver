using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class CachedWordsRepository : ICachedWordsRepository
    {
        public void AddCachedWord(int phraseId, IEnumerable<Anagram> anagrams)
        {
            throw new NotImplementedException();
        }

        public void DeleteCachedWord(int id)
        {
            throw new NotImplementedException();
        }

        public CachedWord GetCachedWord(int id)
        {
            throw new NotImplementedException();
        }

        public IList<CachedWord> GetCachedWords()
        {
            throw new NotImplementedException();
        }
    }
}
