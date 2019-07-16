using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Services
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

        public IEnumerable<UserLog> GetUserLogs()
        {
            var userLogs = _userLogsRepository
                .GetUserLogs()
                .Join(_phrasesRepository.GetPhrases(), log => log.SearchPhraseId, phrase => phrase.Id, (log, phrase)
                => new UserLog
                {
                    UserIp = log.UserIp,
                    SearchPhrase = phrase.Phrase,
                    SearchTime = log.SearchTime,
                    Anagram = ""
                });

            return userLogs;
        }
    }
}
