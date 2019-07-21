using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
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
                throw new ArgumentNullException("argument word is null");

            _wordsDBContext.Words.Add(new WordEntity
            {
                Id = word.Id,
                Word = word.Text
            });

            _wordsDBContext.SaveChanges();
        }

        public void AddWords(params Word[] words)
        {
            if (words == null || words.Length == 0)
                throw new ArgumentNullException("Argument words is null or empty");

            _wordsDBContext.Words.AddRange(words.Select(w => new WordEntity
            {
                Id = w.Id,
                Word = w.Text
            }));

            _wordsDBContext.SaveChanges();
        }

        public void DeleteWord(int id)
        {
            var wordEntity = _wordsDBContext.Words.FirstOrDefault(w => w.Id == id);

            if (wordEntity == null)
                throw new ArgumentException($"word by id of {id} not found");

            _wordsDBContext.Words.Remove(wordEntity);

            _wordsDBContext.SaveChanges();
        }

        public Word GetWord(int id)
        {
            var wordEntity = _wordsDBContext.Words.FirstOrDefault(w => w.Id == id);

            if (wordEntity == null)
                throw new ArgumentException($"word by id of {id} not found");

            return new Word
            {
                Id = wordEntity.Id,
                Text = wordEntity.Word
            };
        }

        public IList<Word> GetWords()
        {
            var wordEntities = _wordsDBContext.Words;
            return wordEntities
                .Select(w => new Word
                {
                    Id = w.Id,
                    Text = w.Word
                }).ToList();
        }
    }
}

