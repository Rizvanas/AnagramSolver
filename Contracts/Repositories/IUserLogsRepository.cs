﻿using Contracts.Entities;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserLogsRepository
    {
        UserLogEntity GetUserLog(int id);
        IEnumerable<UserLogEntity> GetUserLogs();
        bool AddUserLog(UserLogEntity userLog);
    }
}