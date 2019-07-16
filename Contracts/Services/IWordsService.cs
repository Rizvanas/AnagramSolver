using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IWordsService
    {
        List<Word> GetWords(string word);
        List<Word> GetWords(int? page, int pageSize);
        void AddWord(string word);
    }
}
