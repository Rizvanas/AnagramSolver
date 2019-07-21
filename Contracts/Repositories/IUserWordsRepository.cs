using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserWordsRepository
    {
        void AddUserWord(UserWord userWord);
        void AddUserWords(params UserWord[] userWords);
        void DeleteUserWord(int id);
        UserWord GetUserWord(int id);
        IList<UserWord> GetUserWords();
    }
}
