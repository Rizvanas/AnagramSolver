using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class WordsRepository : IWordsRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public WordsRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddWord(Word word)
        {
            if (word == null)
                throw new ArgumentNullException("argument word is null");

            _wordsDB_CFContext.Words.Add(new WordEntity
            {
                WordId = word.Id,
                Word = word.Text
            });

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddWords(params Word[] words)
        {
            if (words == null || words.Length == 0)
                throw new ArgumentNullException("Argument words is null or empty");

            _wordsDB_CFContext.Words.AddRange(words.Select(w => new WordEntity
            {
                WordId = w.Id,
                Word = w.Text
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteWord(int id)
        {
            var wordEntity = _wordsDB_CFContext.Words.FirstOrDefault(w => w.WordId == id);

            if (wordEntity == null)
                throw new ArgumentException($"word by id of {id} not found");

            _wordsDB_CFContext.Words.Remove(wordEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public Word GetWord(int id)
        {
            var wordEntity = _wordsDB_CFContext.Words.FirstOrDefault(w => w.WordId == id);

            if (wordEntity == null)
                throw new ArgumentException($"word by id of {id} not found");

            return new Word
            {
                Id = wordEntity.WordId,
                Text = wordEntity.Word
            };
        }

        public IList<Word> GetWords()
        {
            var wordEntities = _wordsDB_CFContext.Words;
            return wordEntities
                .Select(w => new Word
                {
                    Id = w.WordId,
                    Text = w.Word
                }).ToList();
        }
    }
}
