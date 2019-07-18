using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IWordsRepository
    {
        Word GetWord(int id);
        IList<Word> GetWords();
        bool AddWord(Word word);
        bool AddWords(params Word[] words);
        bool DeleteWord(int id);
    }
}
