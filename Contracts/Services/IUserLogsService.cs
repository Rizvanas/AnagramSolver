﻿using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IUserLogsService
    {
        IEnumerable<UserLog> GetUserLogs();
    }
}
