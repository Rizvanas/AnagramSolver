using Contracts.DTO;

namespace Contracts.Services
{
    public interface IUsersService
    {
        User GetUserFromWord(string word);
    }
}
