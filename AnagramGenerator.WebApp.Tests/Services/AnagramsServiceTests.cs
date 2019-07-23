using AnagramGenerator.WebApp.Services;
using Contracts;
using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Services
{
    [TestFixture]
    public class AnagramsServiceTests
    {
        private IAnagramSolver _anagramSolver;
        private IUsersRepository _usersRepository;
        private IAppConfig _appConfig;
        private IAnagramsService _anagramsService;

        [SetUp]
        public void Setup()
        {
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _usersRepository = Substitute.For<IUsersRepository>();
            _appConfig = Substitute.For<IAppConfig>();
            _anagramsService = new AnagramsService( _anagramSolver, _usersRepository, _appConfig);
        }

        [Test]
        public void GetAnagrams_ReturnsAllAnagramsForAWord()
        {
            _appConfig.GetConfiguration()["FreeSearchesCount"].Returns("5");

            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            _anagramSolver.GetAnagrams(Arg.Is<string>("Dievas"), Arg.Is<User>(_usersRepository.GetUsers().First()))
                .Returns(new List<Anagram>
                {
                    new Anagram {Id = 1, Text = "veidas"},
                    new Anagram {Id = 1, Text = "deivas"},
                    new Anagram {Id = 1, Text = "viedas"}
                });

            var anagrams = _anagramsService.GetAnagrams("Dievas", "::1");

            _usersRepository.Received(2).GetUsers();
            _anagramSolver.Received(1).GetAnagrams(Arg.Is<string>("Dievas"), Arg.Is<User>(_usersRepository.GetUsers().First()));
            anagrams.ShouldNotBeNull();
            anagrams.Count.ShouldBe(3);
            anagrams.ElementAt(0).Text.ShouldBe("veidas");
            anagrams.ElementAt(1).Text.ShouldBe("deivas");
            anagrams.ElementAt(2).Text.ShouldBe("viedas");
        }

        [Test]
        public void GetAnagrams_ThrowsException()
        {
            _appConfig.GetConfiguration()["FreeSearchesCount"].Returns("5");

            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 0},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            _anagramSolver.GetAnagrams(Arg.Is<string>("Dievas"), Arg.Is<User>(_usersRepository.GetUsers().First()))
                .Returns(new List<Anagram>
                {
                    new Anagram {Id = 1, Text = "veidas"},
                    new Anagram {Id = 1, Text = "deivas"},
                    new Anagram {Id = 1, Text = "viedas"}
                });

            Should.Throw<Exception>(() => _anagramsService.GetAnagrams("Dievas", "::1"));
            _usersRepository.Received(2).GetUsers();
            _anagramSolver.Received(0).GetAnagrams(Arg.Is<string>("Dievas"), Arg.Is<User>(_usersRepository.GetUsers().First()));
        }
    }
}
