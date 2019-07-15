using Contracts.Entities;
using Contracts.Repositories;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class AnagramsRepository : IAnagramsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public AnagramsRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public bool AddAnagrams(params AnagramEntity[] anagrams)
        {
            if (!anagrams.Except(_wordsDBContext.Anagrams).Any())
                return false;

            _wordsDBContext.AddRange(anagrams);
            return true;
        }

        public IEnumerable<AnagramEntity> GetAnagrams()
        {
            return _wordsDBContext.Anagrams;
        }

        public IEnumerable<AnagramEntity> GetAnagrams(PhraseEntity phrase)
        {
            return _wordsDBContext.CachedWords
                .Where(p => p.Phrase.Id == phrase.Id)
                .Select(p => new AnagramEntity
                {
                    Id = p.AnagramId,
                    Anagram = p.Anagram.Anagram,
                    CachedWords = _wordsDBContext.CachedWords.ToList()
                });
        }

        public AnagramEntity GetAnagram(int id)
        {
            return _wordsDBContext.Anagrams
                .FirstOrDefault(a => a.Id == id);
        }

        public bool AddAnagram(AnagramEntity anagram)
        {
            if (_wordsDBContext.Anagrams.Contains(anagram))
                return false;

            _wordsDBContext.Anagrams.Add(anagram);
            return true;
        }
    }
}
