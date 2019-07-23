using Contracts;
using Contracts.DTO;
using Contracts.Extensions;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class UserWordsService : IUserWordsService
    {
        private readonly IUserWordsRepository _userWordsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersService _usersService;

        public UserWordsService(
            IUserWordsRepository userWordsRepository,
            IUsersRepository usersRepository,
            IUsersService usersService)
        {
            _userWordsRepository = userWordsRepository;
            _usersRepository = usersRepository;
            _usersService = usersService;
        }

        public void AddUserWord(string word, string userIp)
        {
            var user = _usersRepository
                .GetUsers()
                .FirstOrDefault(u => u.Ip == userIp);

            _userWordsRepository.AddUserWord(new UserWord
            {
                Text = word,
                UserId = user.Id
            });

            _usersService.UpdateUserSearchesCount(user.Id, user.SearchesLeft + 1);
        }

        public void RemoveUserWord(string word, string userIp)
        {
            var wordToRemove = _userWordsRepository
                .GetUserWords()
                .FirstOrDefault(uw => uw.Text.Trim().ToLower() == word.Trim().ToLower());

            if (wordToRemove == null)
                throw new ArgumentException("The word you are trying to delete doesn't exist");

            var user = _usersRepository
                .GetUsers()
                .FirstOrDefault(u => u.Ip == userIp);

            _userWordsRepository.DeleteUserWord(wordToRemove.Id);
            _usersService.UpdateUserSearchesCount(user.Id, user.SearchesLeft - 1);
        }

        public List<UserWord> GetUserWords(int? page, int pageSize)
        {
            page = (page < 1 || page == null) ? 1 : page;

            return _userWordsRepository
                .GetUserWords()
                .Skip((page.Value - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public UserWord GetUserWord(string word)
        {
            return _userWordsRepository
                .GetUserWords()
                .SingleOrDefault(uw => uw.Text.Trim().ToLower() == word.Trim().ToLower());
        }

        public IList<UserWord> GetUserWords(string word)
        {
            return _userWordsRepository
                .GetUserWords()
                .Where(w => w.Text.StartsWith(word))
                .ToList();
        }

        public void UpdateUserWord(int id, string newValue, string userIp)
        {
            var user = _usersRepository
               .GetUsers()
               .FirstOrDefault(u => u.Ip == userIp);

            _userWordsRepository.UpdateUserWord(id, newValue);
            _usersService.UpdateUserSearchesCount(user.Id, user.SearchesLeft + 1);
        }
    }
}
