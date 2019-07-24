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
    public class WordsControllerTests
    {
        private IWordsService _wordsService;
        private IUserWordsService _userWordsService;
        private WordsController _wordsController;

        [SetUp]
        public void Setup()
        {
            _wordsService = Substitute.For<IWordsService>();
            _userWordsService = Substitute.For<IUserWordsService>();
            _wordsController = new WordsController(_wordsService, _userWordsService);
        }

        [Test]
        public void IndexReturnsWordsViewModel_WithPageAndWordsList()
        {

        }

        [Test]
        public void IndexSetsPagingCookie()
        {

        }

        [Test]
        public void SearchReturnsWordsViewModel_WithWordsList()
        {

        }

        [Test] 
        public void SearchReturnsARedirectToWordsController_WhenSearchPhraseIsNull()
        {

        }
    }
}
