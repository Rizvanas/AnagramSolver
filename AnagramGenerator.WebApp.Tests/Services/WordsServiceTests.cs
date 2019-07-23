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
    public class WordsServiceTests
    {
        private IWordsRepository _wordsRepository;
        private IAppConfig _appConfig;
        private IWordsService _wordsService;

        [SetUp]
        public void Setup()
        {
            Substitute.For<IWordsRepository>();
            Substitute.For<IAppConfig>();

            _wordsService = new WordsService(_wordsRepository, _appConfig);
        }

        [Test]
        public void GetWords_WithStringParameter_ReturnsWordsThatStartWithStringParameter()
        {

        }

        [Test]
        public void GetWords_WithPagingParameters_PageEquals2PageSizeEquals2_ReturnsLastTwoElements()
        {

        }

        [Test]
        public void GetWordsForSearch_ReturnsAllWordsThatAreEligibleForAnagramSearch()
        {

        }
    }
}
