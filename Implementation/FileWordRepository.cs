using Core.Domain;
using Core.DTO;
using Implementation.Extensions;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class FileWordRepository : IFileWordRepository
    {
        private readonly IWordLoader _wordLoader;
        private IEnumerable<Word> _words;
        private const string filePath = @"..\AnagramGenerator\zodynas.txt";
        public FileWordRepository(IWordLoader wordLoader)
        {
            _wordLoader = wordLoader;
            ///TODO: take filePath from config file
            _words = _wordLoader.LoadFromFile(filePath).OrderBy(w => w.Text);
        }

        public IEnumerable<Word> GetWords(PaginationFilter filter)
        {
            return _words
                .Skip((filter.Page ?? 1 - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }

            public IEnumerable<string> GetWordsText()
        {
            return _words.Select(w => w.Text);
        }

        public IEnumerable<Word> GetPaginizedWords(PaginationFilter filter)
        {
            return _words
                .AsQueryable()
                .Skip((filter.Page ?? 1 - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }

        public IEnumerable<Word> SearchWords(string phrase)
        {
            phrase = phrase.Replace(" ", "");
            var searchWord = phrase.GetSearchWord(null);

            return _words
                    .Where(w => phrase.GetSearchWord(w.Text) != searchWord
                    && w.Text.Count(c => !Char.IsWhiteSpace(c)) <= searchWord.Length
                    && w.Text.Length >= 2
                    && w.Text.Length >= 1)
                    .OrderByDescending(w => w.Text.Length)
                    .ToList();
        }
    }
}
