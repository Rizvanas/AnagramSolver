using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Services
{
    public interface ICachedWordsService
    {
        void AddCachedWord(Phrase phrase, IEnumerable<Anagram> anagrams);
        IList<Anagram> GetAnagrams(Phrase phrase);
    }
}
