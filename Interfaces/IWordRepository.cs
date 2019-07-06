using Core.Domain;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IWordRepository
    {
        IEnumerable<Word> GetWords();
        IEnumerable<string> GetWordsText();
        IEnumerable<Word> GetPaginizedWords(PaginationFilter filter);
        bool PutWords(string words);
    }
}
