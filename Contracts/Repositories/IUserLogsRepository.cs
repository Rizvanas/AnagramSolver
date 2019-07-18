using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserLogsRepository
    {
        UserLog GetUserLog(int id);
        IList<UserLog> GetUserLogs();
        bool AddUserLog(UserLog userLog);
        bool AddUserLogs(params UserLog[] userLogs);
    }
}
