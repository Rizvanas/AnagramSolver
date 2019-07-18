using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IAnagramsRepository
        {
        Anagram GetAnagram(int id);
        IList<Anagram> GetAnagrams();
        bool AddAnagram(Anagram anagram);
        bool AddAnagrams(params Anagram[] anagrams);
        bool DeleteAnagram(int id);
    }
}
