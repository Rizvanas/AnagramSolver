using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface ICachedWordsRepository 
    {
        IList<CachedWord> GetCachedWords();
        CachedWord GetCachedWord(int id);
        bool AddCachedWord(Phrase phrase, IEnumerable<Anagram> anagrams);
        bool DeleteCachedWord(int id);
    }
}
