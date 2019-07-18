using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IAnagramsRepository
        {
        Anagram GetAnagram(int id);
        IList<Anagram> GetAnagrams();
        void AddAnagram(Anagram anagram);
        void AddAnagrams(params Anagram[] anagrams);
        void DeleteAnagram(int id);
    }
}
