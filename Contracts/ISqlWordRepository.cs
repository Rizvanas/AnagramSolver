using Core.Domain;
using System.Collections.Generic;

namespace Contracts
{
    public interface ISqlWordRepository : IWordRepository
    {
        bool AddCachedWord(Word word, List<Word> anagrams);
        IEnumerable<Word> GetCachedAnagrams(string phrase);
        IEnumerable<Word> GetWords(string searchPhrase);
        bool AddWord(Word word);
    }
}
