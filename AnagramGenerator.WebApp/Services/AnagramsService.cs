﻿using Contracts;
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
    public class AnagramsService : IAnagramsService
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IUsersRepository _usersRepository;
        private readonly IAppConfig _appConfig;

        public AnagramsService(
            IAnagramSolver anagramSolver, 
            IUsersRepository usersRepository,
            IAppConfig appConfig)
        {
            _anagramSolver = anagramSolver;
            _usersRepository = usersRepository;
            _appConfig = appConfig;
        }

        public IList<Anagram> GetAnagrams(string word, string ipAddress)
        {
            var freeSearchesCount = Convert.ToInt32(_appConfig.GetConfiguration()["FreeSearchesCount"]);

            var user = _usersRepository
                .GetUsers()
                .FirstOrDefault(u => u.Ip == ipAddress);

            if (user == null)
            {
                _usersRepository.AddUser(new User { Ip = ipAddress });
                user = _usersRepository
                    .GetUsers()
                    .FirstOrDefault(u => u.Ip == ipAddress);
            }

            if (user.SearchesLeft > 0)
                return _anagramSolver.GetAnagrams(word, user);

            throw new Exception("You exceeded free searched limit, please add new word");
        }
    }
}
