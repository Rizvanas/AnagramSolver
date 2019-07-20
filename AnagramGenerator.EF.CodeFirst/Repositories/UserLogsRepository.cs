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
            var result = _wordsDB_CFContext.UserLog.Add(new UserLogEntity
            {
                Id = userLog.Id,
                UserIp = userLog.UserIp,
                SearchPhraseId = userLog.SearchPhrase.Id,
                SearchTime = userLog.SearchTime
            });

            if (result.State != EntityState.Added)
                throw new InvalidOperationException("Failed to add UserLog");

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddUserLogs(params UserLog[] userLogs)
        {
            _wordsDB_CFContext.UserLog.AddRange(userLogs.Select(ul => 
            new UserLogEntity
            {
                Id = ul.Id,
                UserIp = ul.UserIp,
                SearchPhrase = new PhraseEntity
                {
                    Id = ul.SearchPhrase.Id,
                    Phrase = ul.SearchPhrase.Text
                },
                SearchTime = ul.SearchTime,
                SearchPhraseId = ul.SearchPhrase.Id
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteUserLog(int id)
        {
            var userLogEntity = _wordsDB_CFContext.UserLog.FirstOrDefault(ul => ul.Id == id);

            if (userLogEntity == null)
                throw new InvalidOperationException($"UserLogEntity with id:{id} was not found");

            _wordsDB_CFContext.UserLog.Remove(userLogEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public UserLog GetUserLog(int id)
        {
            var userLogEntity = _wordsDB_CFContext.UserLog.FirstOrDefault(w => w.Id == id);

            if (userLogEntity == null)
                throw new InvalidOperationException($"UserLogEntity with id:{id} was not found");

            return new UserLog
            {
                Id = userLogEntity.Id,
                UserIp = userLogEntity.UserIp,
                SearchPhrase = new Phrase
                {
                    Id = userLogEntity.SearchPhrase.Id,
                    Text = userLogEntity.SearchPhrase.Phrase
                },
                SearchTime = userLogEntity.SearchTime
            };
        }

        public IList<UserLog> GetUserLogs()
        {
            var logEntities = _wordsDB_CFContext.UserLog;

            return logEntities.Select(ul => new UserLog
            {
                Id = ul.Id,
                UserIp = ul.UserIp,
                SearchPhrase = new Phrase
                {
                    Id = ul.SearchPhrase.Id,
                    Text = ul.SearchPhrase.Phrase
                },
                SearchTime = ul.SearchTime
            }).ToList();
        }
    }
}
