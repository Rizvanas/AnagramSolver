using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class UserLogsRepository : IUserLogsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public UserLogsRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public void AddUserLog(UserLog userLog)
        {
            var result = _wordsDBContext.UserLog.Add(new UserLogEntity
            {
                Id = userLog.Id,
                UserIp = userLog.UserIp,
                SearchPhraseId = userLog.SearchPhrase.Id,
                SearchPhrase = new PhraseEntity
                {
                    Id = userLog.Id,
                    Phrase = userLog.SearchPhrase.Text
                },
                SearchTime = userLog.SearchTime
            });

            if (result.State != EntityState.Added)
                throw new InvalidOperationException("Failed to add UserLog");
        }

        public void AddUserLogs(params UserLog[] userLogs)
        {
            _wordsDBContext.UserLog
                .AddRange(userLogs.Select(ul => new UserLogEntity
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
        }

        public void DeleteUserLog(int id)
        {
            var userLogEntity = _wordsDBContext.UserLog.FirstOrDefault(ul => ul.Id == id);

            if (userLogEntity == null)
                throw new InvalidOperationException($"UserLogEntity with id:{id} was not found");

            var result = _wordsDBContext.UserLog.Remove(userLogEntity);
        }

        public UserLog GetUserLog(int id)
        {
            var userLogEntity = _wordsDBContext.UserLog.FirstOrDefault(w => w.Id == id);

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
            var logEntities = _wordsDBContext.UserLog;

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
