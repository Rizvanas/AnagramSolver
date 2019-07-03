using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Interfaces;

namespace Implementation
{
    public class TxtWordLoader : IWordLoader
    {
        public Dictionary<string, string> Load(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var words = new Dictionary<string, string>();
            var regex = new Regex("[.-]|[0-9]");

            foreach (var line in lines)
            {
                var lineArr = line.ToLower().Split('\t');
                if (!regex.IsMatch(lineArr[0]))
                    words.TryAdd(lineArr[0], lineArr[1]);

                if (!regex.IsMatch(lineArr[2]))
                    words.TryAdd(lineArr[2], lineArr[1]);
            }

            return words;
        } 
    }
}
