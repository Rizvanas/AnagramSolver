using Core.Domain;
using Core.DTO;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class WordRepository : IWordRepository
    {
        private readonly IWordLoader _wordLoader;
        private readonly IEnumerable<Word> _words;

        public WordRepository(IWordLoader wordLoader)
        {
            _wordLoader = wordLoader;
            ///TODO: take filePath from config file
            _words = _wordLoader.Load(@"..\AnagramGenerator\zodynas.txt");
        }

        public IEnumerable<Word> GetWords()
        {
            return _words;
        }

        public IEnumerable<string> GetWordsText()
        {
            return _words.Select(w => w.Text);
        }

        public IEnumerable<Word> GetPaginizedWords(PaginationFilter filter)
        {
            return _words
                .AsQueryable()
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }
    }
}
