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

        public void LogUserInfo(Phrase phrase, string ip, int searchTime)
        {
            try
            {
                _userLogsRepository.AddUserLog(new UserLog
                {
                    UserIp = ip,
                    SearchPhrase = phrase,
                    SearchTime = searchTime,
                });
            }
            catch (System.InvalidOperationException)
            {
                throw;
            }
        }
    }
}
