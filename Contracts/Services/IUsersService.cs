using Contracts.DTO;

namespace Contracts.Services
{
    public interface IUsersService
    {
        User GetUserFromWord(string word);
        void UpdateUserSearchesCount(int id, int searchesCount);
    }
}
