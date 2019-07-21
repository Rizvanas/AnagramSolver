using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class UserWordsRepository : IUserWordsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public UserWordsRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public void AddUserWord(UserWord userWord)
        {
            if (userWord == null)
                throw new ArgumentNullException("argument userWord is null");

            _wordsDBContext.UserWords.Add(new UserWordEntity
            {
                Id = userWord.Id,
                Word = userWord.Text,
                UserId = userWord.UserId
            });

            _wordsDBContext.SaveChanges();
        }

        public void AddUserWords(params UserWord[] userWords)
        {
            if (userWords == null || userWords.Length == 0)
                throw new ArgumentNullException("Argument userWords is null or empty");

            _wordsDBContext.UserWords.AddRange(userWords.Select(w => new UserWordEntity
            {
                Id = w.Id,
                Word = w.Text,
                UserId = w.UserId
            }));

            _wordsDBContext.SaveChanges();
        }

        public void DeleteUserWord(int id)
        {
            var userWordEntity = _wordsDBContext.UserWords.FirstOrDefault(w => w.Id == id);

            if (userWordEntity == null)
                throw new ArgumentException($"userWord by id of {id} not found");

            _wordsDBContext.UserWords.Remove(userWordEntity);

            _wordsDBContext.SaveChanges();
        }

        public UserWord GetUserWord(int id)
        {
            var userWordEntity = _wordsDBContext.UserWords.FirstOrDefault(w => w.Id == id);

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
            var userWordEntities = _wordsDBContext.UserWords;
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
