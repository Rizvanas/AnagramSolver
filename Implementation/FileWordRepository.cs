﻿using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.Repositories;
using Contracts.DTO;
using Contracts.Extensions;

namespace Implementation
{
    public class FileWordRepository : IWordsRepository
    {
        private readonly IWordLoader _wordLoader;
        private IList<Word> _words;
        private const string filePath = @"..\AnagramGenerator\zodynas.txt";
        public FileWordRepository(IWordLoader wordLoader)
        {
            _wordLoader = wordLoader;
            _words = _wordLoader
                .LoadFromFile(filePath)
                .OrderBy(w => w.Text);
        }

        public IList<Word> GetWords()
        {
            return _words;
        }

        public IEnumerable<WordEntity> GetWords(PaginationFilter filter)
        {
            return _words
                .Skip((filter.Page ?? 1 - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }

        public IEnumerable<WordEntity> GetWords(PhraseEntity phrase)
        {
             return _words .Where(w => w.Word.StartsWith(phrase.Phrase));
        }

        public WordEntity GetWord(int id)
        {
            return _words.FirstOrDefault(w => w.WordId == id);
        }

        public WordEntity GetWord(string word)
        {
            return _words.FirstOrDefault(w => w.Word.ToLower() == word.ToLower());
        }

        public IEnumerable<WordEntity> GetSearchWords(PhraseEntity phrase)
        {
            var normalizedPhrase = phrase.Phrase.Replace(" ", "");
            var searchWord = normalizedPhrase.GetSearchWord(null);

            return _words
                .Where(w => normalizedPhrase.GetSearchWord(w.Word) != searchWord
                    && w.Word.Count(c => !Char.IsWhiteSpace(c)) <= searchWord.Length
                    && w.Word.Length >= 2
                    && w.Word.Length >= 1)
                .OrderByDescending(w => w.Word.Length)
                .ToList();
        }

        void IWordsRepository.AddWord(WordEntity word)
        {
            throw new NotImplementedException();
        }
    }
}
