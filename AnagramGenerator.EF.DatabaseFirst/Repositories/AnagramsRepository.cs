using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public void AddAnagram(Anagram anagram)
        {
            var result = _wordsDBContext.Anagrams.Add(new AnagramEntity
            {
                Id = anagram.Id,
                Anagram = anagram.Text,
            });

            if (result.State != EntityState.Added)
                throw new InvalidOperationException("Anagram was not added");

            _wordsDBContext.SaveChanges();
        }

        public void AddAnagrams(params Anagram[] anagrams)
        {
            _wordsDBContext.Anagrams
                .AddRange(anagrams.Select(a => new AnagramEntity
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
                throw new InvalidOperationException($"AnagramEntity with id:{id} was not found");

            _wordsDBContext.Anagrams.Remove(anagramEntity);
            _wordsDBContext.SaveChanges();
        }

        public Anagram GetAnagram(int id)
        {
            var anagramEntity = _wordsDBContext.Anagrams.FirstOrDefault(a => a.Id == id);

            if (anagramEntity == null)
                throw new InvalidOperationException($"AnagramEntity with id:{id} was not found");

            return new Anagram
            {
                Id = anagramEntity.Id,
                Text = anagramEntity.Anagram
            };
        }

        public IList<Anagram> GetAnagrams()
        {
            var anagrams = _wordsDBContext.Anagrams;
            return anagrams.Select(ae => new Anagram
            {
                Id = ae.Id,
                Text = ae.Anagram
            }).ToList();
        }
    }
}
