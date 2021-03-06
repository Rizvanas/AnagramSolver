﻿using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IUserLogsService
    {
        IList<UserLog> GetUserLogs();
        void LogUserInfo(Phrase phrase, User user, List<Anagram> anagrams, int searchTime);
    }
}
