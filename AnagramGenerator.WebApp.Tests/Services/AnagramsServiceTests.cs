using AnagramGenerator.WebApp.Services;
using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Services
{
    [TestFixture]
    public class AnagramsServiceTests
    {
        private IAnagramSolver _anagramSolver;
        private IUserWordsRepository _userWordsRepository;
        private IUsersRepository _usersRepository;
        private IAppConfig _appConfig;
        private IAnagramsService _anagramsService;

        [SetUp]
        public void Setup()
        {
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _userWordsRepository = Substitute.For<IUserWordsRepository>();
            _usersRepository = Substitute.For<IUsersRepository>();
            _appConfig = Substitute.For<IAppConfig>();
            _anagramsService = new AnagramsService(
                _anagramSolver, 
                _userWordsRepository,
                _usersRepository, 
                _appConfig);
        }

        [Test]
        public void GetAnagrams_ReturnsAllAnagramsForAWord()
        {

        }

        [Test]
        public void GetAnagrams_ThrowsException()
        {

        }
    }
}
