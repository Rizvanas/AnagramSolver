using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IWordsRepository
    {
        Word GetWord(int id);
        IList<Word> GetWords();
        void AddWord(Word word);
        void AddWords(params Word[] words);
        void DeleteWord(int id);
    }
}
