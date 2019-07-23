using AnagramGenerator.WebApp.Services;
using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Services
{
    [TestFixture]
    public class UsersServiceTests
    {
        private IUserWordsRepository _userWordsRepository;
        private IUsersRepository _usersRepository;
        private IUsersService _usersService;

        [SetUp]
        public void Setup()
        {
            _userWordsRepository = Substitute.For<IUserWordsRepository>();
            _usersRepository = Substitute.For<IUsersRepository>();
            _usersService = new UsersService(_userWordsRepository, _usersRepository);
        }

        [Test]
        public void GetUserFromWord_GetsUserFromStringValue()
        {
            _userWordsRepository.GetUserWords().Returns(new List<UserWord>
            {
                new UserWord{ Id = 1, Text = "Anagrama", UserId = 1 },
                new UserWord{ Id = 2, Text = "Lambda", UserId = 1 },
                new UserWord{ Id = 3, Text = "Šilas", UserId = 2 }
            });

            _usersRepository.GetUser(Arg.Is<int>(1))
                .Returns(new User { Id = 1, Ip = "::1", SearchesLeft = 4 });

            _usersRepository.GetUser(Arg.Is<int>(2))
                .Returns(new User { Id = 2, Ip = "::2", SearchesLeft = 3 });

            var userResult1 = _usersService.GetUserFromWord("Lambda");
            _usersRepository.Received().GetUser(Arg.Is<int>(1));
            userResult1.ShouldNotBeNull();
            userResult1.Id.ShouldBe(1);

            var userResult2 = _usersService.GetUserFromWord("Šilas");
            _usersRepository.Received().GetUser(Arg.Is<int>(2));
            userResult2.ShouldNotBeNull();
            userResult2.Id.ShouldBe(2);
        }

        [Test]
        public void UpdateUserSearchesCount_UpdatesUsersInfo()
        {
        }
    }
}
