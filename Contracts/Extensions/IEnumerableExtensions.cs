using Contracts.DTO;
using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                    yield return element;
            }
        }

        public static IEnumerable<Anagram> ToAnagramsList(this IEnumerable<AnagramEntity> anagramEntities)
        {
            return anagramEntities.Select(a => new Anagram { Text = a.Anagram });
        }

        public static IEnumerable<Word> ToWordsList(this IEnumerable<WordEntity> wordEntities)
        {
            return wordEntities.Select(w => new Word { Text = w.Word });
        }

        public static IEnumerable<Phrase> ToPhrasesList(this IEnumerable<PhraseEntity> phraseEntities)
        {
            return phraseEntities.Select(p => p).
        }
    }
}
