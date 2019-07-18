using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserLogsRepository : IUserLogsRepository
    {
        public void AddUserLog(UserLog userLog)
        {
            throw new NotImplementedException();
        }

        public void AddUserLogs(params UserLog[] userLogs)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserLog(int id)
        {
            throw new NotImplementedException();
        }

        public UserLog GetUserLog(int id)
        {
            throw new NotImplementedException();
        }

        public IList<UserLog> GetUserLogs()
        {
            throw new NotImplementedException();
        }
    }
}
