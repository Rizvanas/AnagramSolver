using AnagramGenerator.WebApp.Services;
using Contracts.DTO;
using Contracts.Repositories;
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
    public class UserLogsServiceTests
    {
        private IUserLogsRepository _userLogsRepository;
        private UserLogsService _userLogsService;

        [SetUp]
        public void Setup()
        {
            _userLogsRepository = Substitute.For<IUserLogsRepository>();
            _userLogsService = new UserLogsService(_userLogsRepository);
        }

        [Test]
        public void GetUserLogs_ShouldGetFullListOfUserLogs()
        {
            _userLogsRepository.GetUserLogs().Returns(new List<UserLog>
            {
                new UserLog
                {
                    Id = 1,
                    SearchTime = 100,
                    Phrase = new Phrase{ Id = 1, Text = "Angela Merkel" },
                    Anagram = new Anagram {Id = 1, Text = "Karamelengel" },
                    User = new User { Id = 1, Ip = "::1", SearchesLeft = 5}
                },
                new UserLog
                {
                    Id = 2,
                    SearchTime = 100,
                    Phrase = new Phrase{ Id = 1, Text = "Angela Merkel" },
                    Anagram = new Anagram {Id = 1, Text = "klare Maengel" },
                    User = new User { Id = 1, Ip = "::1", SearchesLeft = 5}
                },
                new UserLog
                {
                    Id = 3,
                    SearchTime = 100,
                    Phrase = new Phrase{ Id = 1, Text = "Angela Merkel" },
                    Anagram = new Anagram {Id = 1, Text = "gare kamellen" },
                    User = new User { Id = 1, Ip = "::1", SearchesLeft = 5}
                }
            });

            var userLogsResult = _userLogsService.GetUserLogs();

            _userLogsRepository.Received().GetUserLogs();
            userLogsResult.ShouldNotBeNull();
            userLogsResult.ShouldNotBeEmpty();
            userLogsResult.First().ShouldBeOfType(typeof(UserLog));
            userLogsResult.First().Anagram.Text.ShouldBe("Karamelengel");
        }

        [Test]
        public void LogUserInfo_ShouldAddNewUserLog()
        {
            var phrase = new Phrase { Id = 2, Text = "Dievas" };
            var user = new User { Id = 1, Ip = "::1", SearchesLeft = 3 };
            var anagrams = new List<Anagram>()
            {
                new Anagram{Id = 1, Text = "deivas"},
                new Anagram{Id = 2, Text = "veidas"},
                new Anagram{Id = 3, Text = "vis dea"}
            };
            var searchTime = 200;

            _userLogsService.LogUserInfo(phrase, user, anagrams, searchTime);
            _userLogsRepository.Received().AddUserLog(Arg.Any<UserLog>());
            _userLogsRepository.ReceivedCalls().ShouldNotBeEmpty();
            _userLogsRepository.ReceivedCalls().ToList().Count.ShouldBe(3);
        }
    }
}
