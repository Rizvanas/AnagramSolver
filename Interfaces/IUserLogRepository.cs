using Core.DTO;
using Core.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUserLogRepository
    {
        bool AddUserLog(UserLog userLog);
        List<UserLogResponse> GetUserLogs();
    }
}
