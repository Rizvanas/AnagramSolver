using Core.Domain;
using System.Collections.Generic;

namespace Contracts
{
    public interface ISqlWordRepository : IWordRepository
    {
        bool AddCachedWord(Word word, List<Word> anagrams);
        List<Word> GetCachedAnagrams(string phrase);
        List<Word> GetWords(string searchPhrase);
        bool AddWord(Word word);
    }
}
