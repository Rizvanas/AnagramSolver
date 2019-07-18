using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts;
using Contracts.DTO;
using Contracts.Extensions;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public WordsRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public void AddWord(Word word)
        {
            if (word == null)
                throw new ArgumentNullException("Word parameter cannot be null");

            var result = _wordsDBContext.Words.Add(new WordEntity
            {
                WordId = word.Id,
                Word = word.Text
            });
        }

        public void AddWords(params Word[] words)
        {
            if (words == null || words.Length == 0)
                throw new ArgumentNullException("Words cannot be null");

            _wordsDBContext.Words
                .AddRange(words.Select(w => new WordEntity
                {
                    WordId = w.Id,
                    Word = w.Text
                }));
        }

        public void DeleteWord(int id)
        {
            var wordEntity = _wordsDBContext.Words.FirstOrDefault(w => w.WordId == id);

            if (wordEntity == null)
                throw new KeyNotFoundException("Word not found");

            var result = _wordsDBContext.Words.Remove(wordEntity);
        }

        public Word GetWord(int id)
        {
            var wordEntity = _wordsDBContext.Words.FirstOrDefault(w => w.WordId == id);
            
            if (wordEntity == null)
                return null;

            return new Word
            {
                Id = wordEntity.WordId,
                Text = wordEntity.Word
            };
        }

        public IList<Word> GetWords()
        {
            var words = _wordsDBContext.Words;

            return words.Select(we => new Word
            {
                Id = we.WordId,
                Text = we.Word
            }).ToList();
        }
    }
}
