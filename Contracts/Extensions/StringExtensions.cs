using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts.Extensions
{
    public static class StringExtensions
    {
        public static bool IsAnagramTo(this string src, string str)
        {
            if (str == null)
                return false;

            var strLetterTable = src.GetCharFrequencyHist();
            var anagramLetters = str.ToLower()
                                    .ToCharArray()
                                    .Where(c => Char.IsLetterOrDigit(c));

            foreach (var letter in anagramLetters)
            {
                if (strLetterTable.ContainsKey(letter))
                    strLetterTable[letter]--;
                else
                    return false;
            }

            return !strLetterTable.Values.Any(v => v != 0);
        }
        
        public static SortedDictionary<char, int> GetCharFrequencyHist(this string str)
        {
            var letters = new SortedDictionary<char, int>();
            var strArr = str.ToLower()
                            .ToCharArray()
                            .Where(c => Char.IsLetterOrDigit(c));

            foreach (var letter in strArr)
            {
                if (letters.ContainsKey(letter))
                    letters[letter]++;
                else
                    letters[letter] = 1;
            }

            return letters;
        }

        public static string GetSearchWord(this string src, string str)
        {
            if (str == null)
                return src;

            var srcLetters = src.GetCharFrequencyHist();
            var strArr = str.ToLower()
                .ToCharArray()
                .Where(c => Char.IsLetterOrDigit(c));

            foreach (var letter in strArr)
            {
                if(srcLetters.ContainsKey(letter))
                {
                    srcLetters[letter]--;
                    if (srcLetters[letter] < 0)
                        return src;
                }
                else
                {
                    return src;
                }
            }

            var word = new StringBuilder();
            for (int i = 0; i < srcLetters.Count; i++)
            {
                for(int j = 0; j < srcLetters.Values.ElementAt(i); j++)
                {
                    word.Append(srcLetters.Keys.ElementAt(i));
                }
            }

            return word.ToString();
        }
    }
}
