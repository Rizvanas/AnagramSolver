using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Repositories
{
    public interface IUserWordsRepository
    {
        void AddUserWord(UserWord userWord);
        void AddUserWords(params UserWord[] userWords);
        void UpdateUserWord(int id, string text);
        void DeleteUserWord(int id);
        UserWord GetUserWord(int id);
        IList<UserWord> GetUserWords();
    }
}
