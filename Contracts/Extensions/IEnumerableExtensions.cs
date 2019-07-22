using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static int GetUserAddedWordsCount(this IEnumerable<UserWord> userWords, User user)
        {
            return userWords
                .Where(u => u.UserId == user.Id)
                .ToList()
                .Count;
        }

        public static int GetUserSearchedWordsCount(this IEnumerable<UserLog> userLogs, User user)
        {
            var userSearchedWords = userLogs
                .Where(ul => ul.User.Id == user.Id)
                .GroupBy(ul => ul.Phrase.Id).GroupBy(gr => gr.Select(g => g).Select(a => a.Anagram))
                .ToList();

            return userSearchedWords.Count;
        }
    }
}
