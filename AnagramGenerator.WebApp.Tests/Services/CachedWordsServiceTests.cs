using AnagramGenerator.WebApp.Services;
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
    public class CachedWordsServiceTests
    {
        private ICachedWordsRepository _cachedWordsRepository;
        private ICachedWordsService _cachedWordsService;

        [SetUp]
        public void Setup()
        {
            Substitute.For<ICachedWordsRepository>();
            _cachedWordsService = new CachedWordsService(_cachedWordsRepository);
        }

        [Test]
        public void AddCachedWord_AddsNewCachedWordToRepository()
        {

        }

        [Test]
        public void GetAnagrams_GetsCachedAnagramsBasedOnInputPhrase()
        {

        }
    }
}
