using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class AnagramsRepository : IAnagramsRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public AnagramsRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddAnagram(Anagram anagram)
        {
            if (anagram == null)
                throw new ArgumentNullException("argument anagram is null");

            _wordsDB_CFContext.Anagrams.Add(new AnagramEntity
            {
                Id = anagram.Id,
                Anagram = anagram.Text
            });

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddAnagrams(params Anagram[] anagrams)
        {
            if (anagrams == null || anagrams.Length == 0)
                throw new ArgumentNullException("Argument anagrams is null or empty");

            _wordsDB_CFContext.Anagrams.AddRange(anagrams.Select(a => new AnagramEntity
            {
                Id = a.Id,
                Anagram = a.Text
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteAnagram(int id)
        {
            var anagramEntity = _wordsDB_CFContext.Anagrams.FirstOrDefault(a => a.Id == id);

            if (anagramEntity == null)
                throw new ArgumentException($"anagram by id of {id} not found");

            _wordsDB_CFContext.Anagrams.Remove(anagramEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public Anagram GetAnagram(int id)
        {
            var anagramEntity = _wordsDB_CFContext.Anagrams.FirstOrDefault(a => a.Id == id);

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
            var anagramEntities = _wordsDB_CFContext.Anagrams;
            return anagramEntities.Select(a => new Anagram
            {
                Id = a.Id,
                Text = a.Anagram
            }).ToList();
        }
    }
}
