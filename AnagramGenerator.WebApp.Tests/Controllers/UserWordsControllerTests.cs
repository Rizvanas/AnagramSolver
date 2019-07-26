using AnagramGenerator.WebApp.Controllers;
using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Controllers
{
    [TestFixture]
    public class UserWordsControllerTests
    {
        private IUserWordsService _userWordsService;
        private ControllerContext _controllerContext;
        private UserWordsController _userWordsController;

        [SetUp]
        public void Setup()
        {
            _userWordsService = Substitute.For<IUserWordsService>();
            _userWordsController = new UserWordsController(_userWordsService);
            _userWordsController.ControllerContext = new ControllerContext();
            _userWordsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _userWordsController.ControllerContext.HttpContext.Connection.RemoteIpAddress =
                new IPAddress(new byte[] { 127, 0, 0, 1 });
            _userWordsController.Request.Cookies["CurrentPage"].Returns("2");   
        }

        [Test]
        public void IndexReturnsUserWordsViewModel()
        {
            int page = 1;
            int pageSize = 2;
            _userWordsService.GetUserWords(page, pageSize).Returns(GetTestUserWords());
            _userWordsController.Index(page, pageSize);
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

        private List<UserWord> GetTestUserWords()
        {
            return new List<UserWord>
            {
                new UserWord {Id = 1, Text = "Labas", UserId = 1},
                new UserWord {Id = 2, Text = "Vakaras", UserId = 1},
            };
        }
    }
}
