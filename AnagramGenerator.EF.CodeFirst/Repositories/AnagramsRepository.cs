using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class AnagramsRepository : IAnagramsRepository
    {
        public void AddAnagram(Anagram anagram)
        {
            throw new NotImplementedException();
        }

        public void AddAnagrams(params Anagram[] anagrams)
        {
            throw new NotImplementedException();
        }

        public Anagram GetAnagram(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Anagram> GetAnagrams()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Anagram> GetAnagrams(Phrase phrase)
        {
            throw new NotImplementedException();
        }
    }
}
