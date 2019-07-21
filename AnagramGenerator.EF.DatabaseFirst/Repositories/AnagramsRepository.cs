using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class AnagramsRepository : IAnagramsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public AnagramsRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public void AddAnagram(Anagram anagram)
        {
            if (anagram == null)
                throw new ArgumentNullException("argument anagram is null");

            _wordsDBContext.Anagrams.Add(new AnagramEntity
            {
                Id = anagram.Id,
                Anagram = anagram.Text
            });

            _wordsDBContext.SaveChanges();
        }

        public void AddAnagrams(params Anagram[] anagrams)
        {
            if (anagrams == null || anagrams.Length == 0)
                throw new ArgumentNullException("Argument anagrams is null or empty");

            _wordsDBContext.Anagrams.AddRange(anagrams.Select(a => new AnagramEntity
            {
                Id = a.Id,
                Anagram = a.Text
            }));

            _wordsDBContext.SaveChanges();
        }

        public void DeleteAnagram(int id)
        {
            var anagramEntity = _wordsDBContext.Anagrams.FirstOrDefault(a => a.Id == id);

            if (anagramEntity == null)
                throw new ArgumentException($"anagram by id of {id} not found");

            _wordsDBContext.Anagrams.Remove(anagramEntity);

            _wordsDBContext.SaveChanges();
        }

        public Anagram GetAnagram(int id)
        {
            var anagramEntity = _wordsDBContext.Anagrams.FirstOrDefault(a => a.Id == id);

            if (anagramEntity == null)
                throw new ArgumentException($"anagram by id of {id} not found");

            return new Anagram
            {
                Id = anagramEntity.Id,
                Text = anagramEntity.Anagram
            };
        }

        public IList<Anagram> GetAnagrams()
        {
            var anagramEntities = _wordsDBContext.Anagrams;
            return anagramEntities.Select(a => new Anagram
            {
                Id = a.Id,
                Text = a.Anagram
            }).ToList();
        }
    }
}
