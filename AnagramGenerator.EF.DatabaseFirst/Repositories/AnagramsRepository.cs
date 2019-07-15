﻿using Contracts.Entities;
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
            return _wordsDBContext.Anagrams
                .Where(a => a.CachedWords.Any(cw => cw.PhraseId == phrase.Id));
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
