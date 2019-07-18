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

        public bool AddAnagram(Anagram anagram)
        {
            var result = _wordsDBContext.Anagrams.Add(new AnagramEntity
            {
                Anagram =anagram.Text
            });
        }

        public bool AddAnagrams(params Anagram[] anagrams)
        {
            throw new NotImplementedException();
        }

        public Anagram GetAnagram(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Anagram> GetAnagrams()
        {
            throw new NotImplementedException();
        }
    }
}
