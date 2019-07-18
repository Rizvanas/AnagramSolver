using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IWordsService
    {
        IList<Word> GetWords(string word);
        IList<Word> GetWords(int? page, int pageSize);
        bool AddWord(string word);
    }
}
