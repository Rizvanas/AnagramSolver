using Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUserLogRepository
    {
        bool AddUserLog(UserLog userLog);
    }
}
