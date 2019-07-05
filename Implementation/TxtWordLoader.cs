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
                var lineArr = line.ToLower().Split('\t');
                if (!regex.IsMatch(lineArr[0]))
                    words.Add(new Word { Text = lineArr[0], Type = lineArr[1] });

                if (!regex.IsMatch(lineArr[2]))
                    words.Add(new Word { Text = lineArr[2], Type = lineArr[1] });
            }

            return words
                .Where(w => !forbiddenTypes.Contains(w.Type))
                .ToHashSet();
        }
    }
}
