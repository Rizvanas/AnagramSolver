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

namespace AnagramGenerator.WebApp.Tests.Services
{
    [TestFixture]
    public class UserWordsServiceTests
    {
        private IUserWordsRepository _userWordsRepository;
        private IUsersRepository _usersRepository;
        private IUsersService _usersService;
        private IUserWordsService _userWordsService;

        [SetUp]
        public void Setup()
        {
            _userWordsRepository = Substitute.For<IUserWordsRepository>();
            _usersRepository = Substitute.For<IUsersRepository>();
            _usersService = Substitute.For<IUsersService>();
            _userWordsService = new UserWordsService(_userWordsRepository, _usersRepository, _usersService);
        }

        [Test]
        public void AddUserWord_AddsNewUserWordToUserWordsRepository()
        {
            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            var testUser = _usersRepository.GetUsers().First();
            var testUserWord = new UserWord { Text = "Testas", UserId = testUser.Id };

            _userWordsService.AddUserWord(testUserWord.Text, testUser.Ip);

            _usersRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _usersRepository.Received(2).GetUsers();
            _userWordsRepository.Received(1).AddUserWord(Arg.Any<UserWord>());
        }

        [Test]
        public void RemoveUserWord_RemovesUserWordFromUserWordsRepository()
        {
            _userWordsRepository.GetUserWords().Returns(new List<UserWord>
            {
                new UserWord {Id = 1, UserId = 1, Text = "Unit"},
                new UserWord {Id = 2, UserId = 1, Text = "Testas"},
                new UserWord {Id = 3, UserId = 2, Text = "Mockinimo"},
                new UserWord {Id = 3, UserId = 2, Text = "Karkasas"}
            });

            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });


            var testUser = _usersRepository.GetUsers().First();
            var testWord = _userWordsRepository.GetUserWords().First();

            _userWordsService.RemoveUserWord(testWord.Text, testUser.Ip);

            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.Received(1).DeleteUserWord(Arg.Is(testWord.Id));
        }

        [Test]
        public void UpdateUserWord_UpdatesExistingWordInUserWordsRepository()
        {
            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            var testUser = _usersRepository.GetUsers().First();
            var testWord = new UserWord { Id = 1, UserId = 1, Text = "Unit" };
            var updateText = "updateText";

            _userWordsService.UpdateUserWord(testWord.Id, updateText, testUser.Ip);

            _usersRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.Received(1).UpdateUserWord(Arg.Is(testWord.Id), Arg.Is(updateText));
            _usersRepository.Received(2).GetUsers();
        }

        [Test]
        public void AddUserWord_DecrementsUserSearchesCountByOne()
        {
            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            var testUser = _usersRepository.GetUsers().First();
            var testUserWord = new UserWord { Text = "Testas", UserId = testUser.Id };

            _userWordsService.AddUserWord(testUserWord.Text, testUser.Ip);

            _usersRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _usersRepository.Received(2).GetUsers();
            _usersService.Received().UpdateUserSearchesCount(Arg.Is(testUser.Id), Arg.Is(testUser.SearchesLeft + 1));
        }

        [Test]
        public void RemoveUserWord_DecrementsUserSearchesCountByOne()
        {
            _userWordsRepository.GetUserWords().Returns(new List<UserWord>
            {
                new UserWord {Id = 1, UserId = 1, Text = "Unit"},
                new UserWord {Id = 2, UserId = 1, Text = "Testas"},
                new UserWord {Id = 3, UserId = 2, Text = "Mockinimo"},
                new UserWord {Id = 3, UserId = 2, Text = "Karkasas"}
            });

            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });


            var testUser = _usersRepository.GetUsers().First();
            var testWord = _userWordsRepository.GetUserWords().First();

            _userWordsService.RemoveUserWord(testWord.Text, testUser.Ip);

            _usersRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _usersService.ReceivedCalls().ShouldNotBeNull();
            _usersService.Received().UpdateUserSearchesCount(Arg.Is(testUser.Id), Arg.Is(testUser.SearchesLeft - 1));
        }

        [Test]
        public void UpdateUserWord_IncrementsUserSearchesCountByOne()
        {
            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            var testUser = _usersRepository.GetUsers().First();
            var testWord = new UserWord { Id = 1, UserId = 1, Text = "Unit" };
            var updateText = "updateText";

            _userWordsService.UpdateUserWord(testWord.Id, updateText, testUser.Ip);

            _usersRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _usersService.Received().UpdateUserSearchesCount(Arg.Is(testUser.Id), Arg.Is(testUser.SearchesLeft + 1));
        }

        [Test]
        public void RemoveUserWord_ThrowsArgumentException()
        {
            _userWordsRepository.GetUserWords().Returns(new List<UserWord>
            {
                new UserWord {Id = 1, UserId = 1, Text = "Unit"},
                new UserWord {Id = 2, UserId = 1, Text = "Testas"},
                new UserWord {Id = 3, UserId = 2, Text = "Mockinimo"},
                new UserWord {Id = 3, UserId = 2, Text = "Karkasas"}
            });

            _usersRepository.GetUsers().Returns(new List<User>
            {
                new User{Id = 1, Ip = "::1", SearchesLeft = 5},
                new User{Id = 2, Ip = "::2", SearchesLeft = 4},
                new User{Id = 3, Ip = "::3", SearchesLeft = 3},
                new User{Id = 4, Ip = "::4", SearchesLeft = 2}
            });

            var testUser = _usersRepository.GetUsers().First();
            var testWord = new UserWord { Id = 100, Text = "Random word" };

            Should.Throw<ArgumentException>(() => _userWordsService.RemoveUserWord(testWord.Text, testUser.Ip));
            _userWordsRepository.ReceivedCalls().ShouldNotBeNull();
            _userWordsRepository.Received(1).GetUserWords();
        }
    }
}
