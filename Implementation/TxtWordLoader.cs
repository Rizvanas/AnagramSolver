using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Domain;
using Interfaces;

namespace Implementation
{
    public class TxtWordLoader : IWordLoader
    {
        public IEnumerable<Word> Load(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var words = new HashSet<Word>();
            var regex = new Regex("[.-]|[0-9]");
            var forbiddenTypes = new List<string> { "sutr", "dll", "akronim" };

            foreach (var line in lines)
            {
                var lineList = line.ToLower().Split('\t').ToList();

                var firstElem = lineList.ElementAtOrDefault(0);
                var secondElem = lineList.ElementAtOrDefault(1);
                var thirdElem = lineList.ElementAtOrDefault(2);

                if (firstElem != null && !regex.IsMatch(firstElem))
                    words.Add(new Word { Text = firstElem, Type = secondElem });

                if (thirdElem != null && !regex.IsMatch(thirdElem))
                    words.Add(new Word { Text = thirdElem, Type = secondElem });
            }

            return words
                .Where(w => !forbiddenTypes.Contains(w.Type))
                .ToHashSet();
        }
    }
}
