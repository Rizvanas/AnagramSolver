using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IAnagramsRepository
    {
        bool AddAnagrams(params AnagramEntity[] anagrams);
        IEnumerable<AnagramEntity> GetAnagrams();
        IEnumerable<AnagramEntity> GetAnagrams(PhraseEntity phrase);
        AnagramEntity GetAnagram(int id);
        bool AddAnagram(AnagramEntity anagram);
    }
}
