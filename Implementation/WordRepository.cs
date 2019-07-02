using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class WordRepository : IWordRepository
    {
        private readonly Dictionary<string, string> _words;
        public WordRepository(IWordLoader wordLoader)
        {
            _words = wordLoader.Load(@"..\AnagramGenerator\zodynas.txt");
        }

        public Dictionary<string, string> GetWords()
        {
            return _words;
        }
    }
}
