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
        private readonly IUserLogsRepository _userLogsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAppConfig _appConfig;

        public UserWordsService(
            IUserWordsRepository userWordsRepository, 
            IUserLogsRepository userLogsRepository, 
            IUsersRepository usersRepository,
            IAppConfig appConfig)
        {
            _userWordsRepository = userWordsRepository;
            _userLogsRepository = userLogsRepository;
            _usersRepository = usersRepository;
            _appConfig = appConfig;
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
        }

        public void RemoveUserWord(string word)
        {
            var wordToRemove = _userWordsRepository
                .GetUserWords()
                .FirstOrDefault(uw => uw.Text.Trim().ToLower() == word.Trim().ToLower());

            if (wordToRemove == null)
                throw new ArgumentException("The word you are trying to delete doensn't exist");

            _userWordsRepository.DeleteUserWord(wordToRemove.Id);
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

        public void UpdateUserWord(int id, string newValue)
        {
            _userWordsRepository.UpdateUserWord(id, newValue);
        }
    }
}
