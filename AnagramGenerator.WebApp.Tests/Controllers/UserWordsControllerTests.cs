using AnagramGenerator.WebApp.Controllers;
using Contracts.Services;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Controllers
{
    [TestFixture]
    public class UserWordsControllerTests
    {
        private IUserWordsService _userWordsService;
        private UserWordsController _userWordsController;

        [SetUp]
        public void Setup()
        {
            _userWordsService = Substitute.For<IUserWordsService>();
            _userWordsController = new UserWordsController(_userWordsService);
        }

        [Test]
        public void IndexReturnsUserWordsViewModel()
        {

        }

        [Test]
        public void IndexSetsPagingCookie()
        {
            
        }

        [Test]
        public void UpdateReturnsUserWordsUpdateViewModel()
        {

        }

        [Test]
        public void UpdateListReturnsARedirectToUserWordsControllersIndexAction()
        {

        }

        [Test]
        public void RemoveReturnsARedirectToUserWordsControllersIndexAction()
        {

        }

        [Test]
        public void SearchReturnsARedirectToUserWordsControllersIndexAction_WhenSearchPhraseIsNull()
        {

        }

        [Test]
        public void SearchReturnsUserWordsViewModel_WithAListOfUserWords_WhenSearchPhraseIsNotNull()
        {

        }

        [Test]
        public void ChangeReturnsARedirectToUserWordsControllersIndexAction_WhenWordIsNotNull()
        {

        }

        [Test]
        public void ChangeReturnsNoContent_WhenWordIsNull()
        {

        }

    }
}
