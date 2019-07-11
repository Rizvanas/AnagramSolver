using Core.Domain;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ISqlWordRepository : IWordRepository
    {
        bool AddWord(Word word);
        bool AddCachedWord(Word word, List<string> anagrams);
        List<string> GetCachedAnagrams(string phrase);
    }
}
