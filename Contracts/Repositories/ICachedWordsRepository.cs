using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface ICachedWordsRepository 
    {
        IList<CachedWord> GetCachedWords();
        CachedWord GetCachedWord(int id);
        void AddCachedWord(Phrase phrase, IEnumerable<Anagram> anagrams);
        void DeleteCachedWord(int id);
    }
}
