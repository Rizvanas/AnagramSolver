﻿using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IUserWordsService
    {
        void AddUserWord(string word, string userIp);
        List<UserWord> GetUserWords(int? page, int pageSize);
        UserWord GetUserWord(string word);
        IList<UserWord> GetUserWords(string word);
        void RemoveUserWord(string word, string userIp);
        void RemoveUserWord(int id, string userIp);
        void UpdateUserWord(int id, string newValue, string userIp);
    }
}
