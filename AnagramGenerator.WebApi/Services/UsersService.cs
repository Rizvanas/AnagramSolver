using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserWordsRepository _userWordsRepository;
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUserWordsRepository userWordsRepository, IUsersRepository usersRepository)
        {
            _userWordsRepository = userWordsRepository;
            _usersRepository = usersRepository;
        }

        public User GetUserFromWord(string word)
        {
            var userWord = _userWordsRepository
                .GetUserWords()
                .SingleOrDefault(uw => uw.Text.Trim().ToLower() == word.Trim().ToLower());

            return _usersRepository.GetUser(userWord.UserId);
        }

        public void UpdateUserSearchesCount(int id, int searchesCount)
        {
            _usersRepository.UpdateUser(new User {  Id = id, SearchesLeft = searchesCount });
        }
    }
}
