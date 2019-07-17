using Contracts.DTO;
using Contracts.Entities;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserLogsRepository
    {
        UserLog GetUserLog(int id);
        IEnumerable<UserLog> GetUserLogs();
        void AddUserLog(UserLog userLog);
    }
}
