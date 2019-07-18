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

        public bool AddWord(Word word)
        {
            var result = _wordsDBContext.Words.Add(new WordEntity
            {
                WordId = word.Id,
                Word = word.Text
            });

            return result.State == EntityState.Added;
        }

        public bool AddWords(params Word[] words)
        {
            _wordsDBContext.Words
                .AddRange(words.Select(w => new WordEntity
                {
                    WordId = w.Id,
                    Word = w.Text
                }));

            return true;
        }

        public bool DeleteWord(int id)
        {
            var wordEntity = _wordsDBContext.Words.FirstOrDefault(w => w.WordId == id);
            var result = _wordsDBContext.Words.Remove(wordEntity);

            return result.State == EntityState.Deleted;
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
