using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class AnagramsService : IAnagramsService
    {
        private readonly ICachedWordsRepository _cachedWordsRepository;
        private readonly IAnagramsRepository _anagramsRepository;

        public AnagramsService(ICachedWordsRepository cachedWordsRepository, IAnagramsRepository anagramsRepository)
        {
            _cachedWordsRepository = cachedWordsRepository;
            _anagramsRepository = anagramsRepository;
        }

        public IList<Anagram> GetAnagrams(Phrase phrase)
        {
            return _cachedWordsRepository.GetCachedWords()
                .Where(p => p.Phrase.Id == phrase.Id)
                .Select(p => new Anagram
                {
                    Id = p.AnagramId,
                    Text = p.Anagram.Text,
                }).ToList();
        }
    }
}
