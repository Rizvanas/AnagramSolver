using Contracts.Repositories;
using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class UserLogsRepository : IUserLogsRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public UserLogsRepository (WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public UserLogEntity GetUserLog(int id)
        {
            return _wordsDBContext.UserLog
                .FirstOrDefault(userLog => userLog.Id == id);
        }

        public IEnumerable<UserLogEntity> GetUserLogs()
        {
            return _wordsDBContext.UserLog;
        }

        public void AddUserLog(UserLogEntity userLog)
        {
            _wordsDBContext.UserLog.Add(userLog);
            _wordsDBContext.SaveChanges();
        }
    }
}
