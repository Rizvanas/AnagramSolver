using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IAnagramsRepository
    {
        void AddAnagrams(params Anagram[] anagrams);
        IEnumerable<Anagram> GetAnagrams();
        IEnumerable<Anagram> GetAnagrams(Phrase phrase);
        Anagram GetAnagram(int id);
        void AddAnagram(Anagram anagram);
    }
}
