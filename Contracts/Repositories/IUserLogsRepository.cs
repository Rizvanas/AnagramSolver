using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserLogsRepository
    {
        UserLog GetUserLog(int id);
        IList<UserLog> GetUserLogs();
        void AddUserLog(UserLog userLog);
        void AddUserLogs(params UserLog[] userLogs);
        void DeleteUserLog(int id);
    }
}
