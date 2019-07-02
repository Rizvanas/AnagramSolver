using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces;

namespace Implementation
{
    public class TxtWordLoader : IWordLoader
    {
        public Dictionary<string, string> Load(string filePath)
        {
            var lines = File.ReadLines(filePath);
            var words = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                var lineArr = line.Split('\t');
                words.TryAdd(lineArr[0], lineArr[1]);
                words.TryAdd(lineArr[2], lineArr[1]);
            }

            //var wordGroups = words.GroupBy(wg => wg.Value);
            return words;
        } 
    }
}
