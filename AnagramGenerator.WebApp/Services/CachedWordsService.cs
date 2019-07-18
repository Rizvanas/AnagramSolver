using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class CachedWordsService : ICachedWordsService
    {
        private readonly ICachedWordsRepository _cachedWordsRepository;

        public CachedWordsService(ICachedWordsRepository cachedWordsRepository)
        {
            _cachedWordsRepository = cachedWordsRepository;
        }

        public void AddCachedWord(Phrase phrase, IEnumerable<Anagram> anagrams)
        {
            _cachedWordsRepository.AddCachedWord(phrase.Id, anagrams);
        }
    }
}
