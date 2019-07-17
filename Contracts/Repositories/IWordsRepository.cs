using Contracts.DTO;
using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IWordsRepository
    {
        IEnumerable<Word> GetWords();
        IEnumerable<Word> GetWords(PaginationFilter filter);
        IEnumerable<Word> GetWords(Phrase phrase);
        Word GetWord(int id);
        Word GetWord(string word);
        IEnumerable<Word> GetSearchWords(Phrase phrase);
        void AddWord(Word word);
    }
}
