using Core.Domain;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class WordRepository : IWordRepository
    {
        private readonly IEnumerable<Word> _words;
        public WordRepository(IWordLoader wordLoader)
        {
            _words = wordLoader.Load(@"..\AnagramGenerator\zodynas.txt");
        }

        public IEnumerable<Word> GetWords()
        {
            return _words;
        }
    }
}
