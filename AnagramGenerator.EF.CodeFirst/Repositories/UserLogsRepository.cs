using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserLogsRepository : IUserLogsRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public UserLogsRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddUserLog(UserLog userLog)
        {
            var result = _wordsDB_CFContext.UserLogs.Add(new UserLogEntity
            {
                Id = userLog.Id,
                UserId = userLog.User.Id,
                PhraseId = userLog.Phrase.Id,
                AnagramId = userLog.Anagram.Id,
                SearchTime = userLog.SearchTime
            });

            if (result.State != EntityState.Added)
                throw new InvalidOperationException("Failed to add UserLog");

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddUserLogs(params UserLog[] userLogs)
        {
            _wordsDB_CFContext.UserLogs.AddRange(userLogs.Select(ul =>
            new UserLogEntity
            {
                Id = ul.Id,
                UserId = ul.User.Id,
                PhraseId = ul.Phrase.Id,
                SearchTime = ul.SearchTime,
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteUserLog(int id)
        {
            var userLogEntity = _wordsDB_CFContext.UserLogs.FirstOrDefault(ul => ul.Id == id);

            if (userLogEntity == null)
                throw new InvalidOperationException($"UserLogEntity with id:{id} was not found");

            _wordsDB_CFContext.UserLogs.Remove(userLogEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public UserLog GetUserLog(int id)
        {
            var userLogEntity = _wordsDB_CFContext.UserLogs.FirstOrDefault(ul => ul.Id == id);

            if (userLogEntity == null)
                throw new InvalidOperationException($"UserLogEntity with id:{id} was not found");

            return new UserLog
            {
                Id = userLogEntity.Id,
                User = new User
                {
                    Id = userLogEntity.UserId,
                    Ip = userLogEntity.User.Ip
                },
                Phrase = new Phrase
                {
                    Id = userLogEntity.Phrase.Id,
                    Text = userLogEntity.Phrase.Phrase
                },
                Anagram = new Anagram
                {
                    Id = userLogEntity.AnagramId,
                    Text = userLogEntity.Anagram.Anagram
                },
                SearchTime = userLogEntity.SearchTime
            };
        }

        public IList<UserLog> GetUserLogs()
        {
            var logEntities = _wordsDB_CFContext.UserLogs;

            return logEntities.Select(ul => new UserLog
            {
                Id = ul.Id,
                User = new User
                {
                    Id = ul.UserId,
                    Ip = ul.User.Ip
                },
                Phrase = new Phrase
                {
                    Id = ul.Phrase.Id,
                    Text = ul.Phrase.Phrase
                },
                Anagram = new Anagram
                {
                    Id = ul.AnagramId,
                    Text = ul.Anagram.Anagram
                },
                SearchTime = ul.SearchTime
            }).ToList();
        }
    }
}
