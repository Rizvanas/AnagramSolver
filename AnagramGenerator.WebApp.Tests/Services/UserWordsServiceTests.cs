using AnagramGenerator.WebApp.Services;
using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using NSubstitute;
using NUnit.Framework;

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
            Substitute.For<IUserWordsRepository>();
            Substitute.For<IUsersRepository>();
            Substitute.For<IUsersService>();

            _userWordsService = new UserWordsService(
                _userWordsRepository,
                _usersRepository,
                _usersService);
        }

        [Test]
        public void AddUserWord_AddsNewUserWordToUserWordsRepository()
        {

        }

        [Test]
        public void RemoveUserWord_RemovesUserWordFromUserWordsRepository()
        {

        }

        [Test]
        public void UpdateUserWord_UpdatesExistingWordInUserWordsRepository()
        {

        }

        [Test]
        public void AddUserWord_DecrementsUserSearchesCountByOne()
        {

        }

        [Test]
        public void RemoveUserWord_IncrementsUserSearchesCountByOne()
        {

        }

        [Test]
        public void UpdateUserWord_IncrementsUserSearchesCountByOne()
        {

        }

        [Test]
        public void RemoveUserWord_ThrowsArgumentException()
        {

        }
    }
}
