using Contracts.Repositories;
using System.Collections.Generic;

namespace Contracts
{
    public interface ISqlWordRepository : IWordsRepository
    {
        bool AddCachedWord(Wro word, List<Word> anagrams);
        IEnumerable<Word> GetCachedAnagrams(string phrase);
        IEnumerable<Word> GetWords(string searchPhrase);
        bool AddWord(Word word);
    }
}
