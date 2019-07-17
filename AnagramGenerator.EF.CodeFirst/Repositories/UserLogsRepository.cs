using Contracts.DTO;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class UserLogsRepository : IUserLogsRepository
    {
        public void AddUserLog(UserLog userLog)
        {
            throw new NotImplementedException();
        }

        public UserLog GetUserLog(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserLog> GetUserLogs()
        {
            throw new NotImplementedException();
        }
    }
}
