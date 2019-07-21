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

        public List<UserWord> GetUserWords()
        {
            return _userWordsRepository.GetUserWords().ToList();
        }
    }
}
