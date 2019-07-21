using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.WebApp.Services
{
    public class UserLogsService : IUserLogsService
    {
        private readonly IUserLogsRepository _userLogsRepository;

        public UserLogsService(IUserLogsRepository userLogsRepository)
        {
            _userLogsRepository = userLogsRepository;
        }

        public IList<UserLog> GetUserLogs()
        {
            var userLogs = _userLogsRepository.GetUserLogs();

            return userLogs;
        }

        public void LogUserInfo(Phrase phrase, User user, List<Anagram> anagrams, int searchTime)
        {
            foreach(var anagram in anagrams)
            {
                _userLogsRepository.AddUserLog(new UserLog
                { Phrase = phrase,
                    User = user,
                    Anagram = anagram,
                    SearchTime = searchTime
                });
            }
        }
    }
}
