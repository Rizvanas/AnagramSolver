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
        void AddCachedWord(CachedWordEntity cachedWord);
        void AddCachedWord(PhraseEntity phrase, IEnumerable<AnagramEntity> anagrams);
    }
}
