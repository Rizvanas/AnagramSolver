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
                 return userLogs
                .Where(u => u.User.Id == user.Id)
                .ToList()
                .Count;
        }
    }
}
