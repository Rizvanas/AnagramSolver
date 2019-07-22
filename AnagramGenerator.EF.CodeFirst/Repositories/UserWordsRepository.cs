using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserWordsRepository : IUserWordsRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public UserWordsRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddUserWord(UserWord userWord)
        {
            if (userWord == null)
                throw new ArgumentNullException("argument userWord is null");

            _wordsDB_CFContext.UserWords.Add(new UserWordEntity
            {
                Id = userWord.Id,
                Word = userWord.Text,
                UserId = userWord.UserId
            });

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddUserWords(params UserWord[] userWords)
        {
            if (userWords == null || userWords.Length == 0)
                throw new ArgumentNullException("Argument userWords is null or empty");

            _wordsDB_CFContext.UserWords.AddRange(userWords.Select(w => new UserWordEntity
            {
                Id = w.Id,
                Word = w.Text,
                UserId = w.UserId
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void UpdateUserWord(int id, string text)
        {
            var userWordEntity = _wordsDB_CFContext.UserWords.FirstOrDefault(w => w.Id == id);
            userWordEntity.Word = text;

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteUserWord(int id)
        {
            var userWordEntity = _wordsDB_CFContext.UserWords.FirstOrDefault(w => w.Id == id);

            if (userWordEntity == null)
                throw new ArgumentException($"userWord by id of {id} not found");

            _wordsDB_CFContext.UserWords.Remove(userWordEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public UserWord GetUserWord(int id)
        {
            var userWordEntity = _wordsDB_CFContext.UserWords.FirstOrDefault(w => w.Id == id);

            if (userWordEntity == null)
                throw new ArgumentException($"userWord by id of {id} not found");

            return new UserWord
            {
                Id = userWordEntity.Id,
                Text = userWordEntity.Word,
                UserId = userWordEntity.UserId
            };
        }

        public IList<UserWord> GetUserWords()
        {
            var userWordEntities = _wordsDB_CFContext.UserWords;
            return userWordEntities
                .Select(w => new UserWord
                {
                    Id = w.Id,
                    Text = w.Word,
                    UserId = w.UserId
                }).ToList();
        }
    }
}
