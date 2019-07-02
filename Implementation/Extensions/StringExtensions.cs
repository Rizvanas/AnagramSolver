using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Extensions
{
    public static class StringExtensions
    {
        public static bool IsAnagramTo(this string src, string str)
        {
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
    }
}
