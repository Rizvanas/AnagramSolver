using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface ICachedWordsRepository
    {
        IEnumerable<CachedWordEntity> GetCachedWords();
        IEnumerable<CachedWordEntity> GetCachedWords(PhraseEntity phrase);
        IEnumerable<CachedWordEntity> GetCachedWords(AnagramEntity anagram);
        bool AddCachedWord(CachedWordEntity cachedWord);
        bool AddCachedWord(PhraseEntity phrase, IEnumerable<AnagramEntity> anagrams);
    }
}
