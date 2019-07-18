using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface ICachedWordsRepository 
    {
        IList<CachedWord> GetCachedWords();
        CachedWord GetCachedWord(int id);
        void AddCachedWord(int phraseId, IEnumerable<Anagram> anagrams);
        void DeleteCachedWord(int id);
    }
}
