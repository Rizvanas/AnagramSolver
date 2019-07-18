using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserLogsRepository : IUserLogsRepository
    {
        public bool AddUserLog(UserLog userLog)
        {
            throw new NotImplementedException();
        }

        public bool AddUserLogs(params UserLog[] userLogs)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserLog(int id)
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
