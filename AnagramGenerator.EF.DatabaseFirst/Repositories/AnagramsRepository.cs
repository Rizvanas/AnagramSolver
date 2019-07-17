using Contracts.DTO;
using Contracts.Entities;
using Contracts.Repositories;
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

        public void AddAnagrams(params Anagram[] anagrams)
        {
            _wordsDBContext.Anagrams.AddRange(anagrams);
            _wordsDBContext.SaveChanges();
        }

        public IEnumerable<Anagram> GetAnagrams()
        {
            return _wordsDBContext.Anagrams;
        }

        public IEnumerable<Anagram> GetAnagrams(Phrase phrase)
        {
            return _wordsDBContext.Anagrams
                .Where(a => a.CachedWords.Any(cw => cw.PhraseId == phrase.Id));
        }

        public Anagram GetAnagram(int id)
        {
            return _wordsDBContext.Anagrams
                .FirstOrDefault(a => a.Id == id);
        }

        public void AddAnagram(Anagram anagram)
        {
            _wordsDBContext.Anagrams.Add(anagram);
            _wordsDBContext.SaveChanges();
        }
    }
}
