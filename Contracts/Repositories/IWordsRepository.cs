using Contracts.DTO;
using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IWordsRepository
    {
        IEnumerable<WordEntity> GetWords();
        IEnumerable<WordEntity> GetWords(PaginationFilter filter);
        IEnumerable<WordEntity> GetWords(PhraseEntity phrase);
        WordEntity GetWord(int id);
        WordEntity GetWord(string word);
        IEnumerable<WordEntity> GetSearchWords(PhraseEntity phrase);
        void AddWord(WordEntity word);
    }
}
