using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IUserWordsService
    {
        void AddUserWord(string word, string userIp);
        List<UserWord> GetUserWords();
    }
}
