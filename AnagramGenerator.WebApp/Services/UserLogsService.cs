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
        private readonly IPhrasesRepository _phrasesRepository;

        public UserLogsService(IUserLogsRepository userLogsRepository, IPhrasesRepository phrasesRepository)
        {
            _userLogsRepository = userLogsRepository;
            _phrasesRepository = phrasesRepository;
        }

        public IList<UserLog> GetUserLogs()
        {
            var userLogs = _userLogsRepository.GetUserLogs();
            return userLogs;
        }
    }
}
