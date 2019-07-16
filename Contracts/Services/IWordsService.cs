using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IWordsService
    {
        IEnumerable<Word> GetWords(string word);
        IEnumerable<Word> GetWords(int? page, int pageSize, string pagingCookie);
        void AddWord(string word);
    }
}
