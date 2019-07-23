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
    public class WordsServiceTests
    {
        private IWordsRepository _wordsRepository;
        private IAppConfig _appConfig;
        private IWordsService _wordsService;

        [SetUp]
        public void Setup()
        {
            _wordsRepository = Substitute.For<IWordsRepository>();
            _appConfig = Substitute.For<IAppConfig>();

            _wordsService = new WordsService(_wordsRepository, _appConfig);
        }

        [Test]
        public void GetWords_WithStringParameter_ReturnsWordsThatStartWithStringParameter()
        {
            _wordsRepository.GetWords().Returns(new List<Word>
            {
                new Word{Id = 1, Text = "Labas"},
                new Word{Id = 2, Text = "Laibas"},
                new Word{Id = 3, Text = "Lalala"},
                new Word{Id = 4, Text = "Albas"},
            });

            var matchingWordsResult = _wordsService.GetWords("la");

            _wordsRepository.Received().GetWords();
            _wordsRepository.ReceivedCalls().ToList().Count.ShouldBe(1);
            matchingWordsResult.ShouldNotBeEmpty();
            matchingWordsResult.First().Text.ShouldBe("Labas");
            matchingWordsResult.Last().Text.ShouldBe("Lalala");
        }

        [Test]
        public void GetWords_WithPagingParameters_PageEquals2PageSizeEquals2_ReturnsLastTwoElements()
        {
            _wordsRepository.GetWords().Returns(new List<Word>
            {
                new Word{Id = 1, Text = "Labas"},
                new Word{Id = 2, Text = "Laibas"},
                new Word{Id = 3, Text = "Lalala"},
                new Word{Id = 4, Text = "Albas"},
            });

            var wordsResult = _wordsService.GetWords(2, 2);

            _wordsRepository.Received(1).GetWords();
            wordsResult.ShouldNotBeNull();
            wordsResult.First().Text.ShouldBe("Lalala");
            wordsResult.Last().Text.ShouldBe("Albas");
        }

        [Test]
        public void GetWordsForSearch_ReturnsAllWordsThatAreEligibleForAnagramSearch()
        {
            _appConfig.GetConfiguration()
                .GetSection("ConstantValues")["MinWordLength"]
                .Returns("2");

            _wordsRepository.GetWords().Returns(new List<Word>
            {
                new Word{Id = 1, Text = "Lvbds"},
                new Word{Id = 2, Text = "Vidas"},
                new Word{Id = 3, Text = "Sabal"},
                new Word{Id = 4, Text = "vaida"},
                new Word{Id = 5, Text = "Lvbdr"},
            });

            var wordsResult = _wordsService.GetWordsForSearch("Labas Vaida");

            _wordsRepository.Received(1).GetWords();
            wordsResult.ShouldNotBeNull();
            wordsResult.Count.ShouldBe(4);
            wordsResult.First().Text.ShouldBe("Lvbds");
            wordsResult.Last().Text.ShouldBe("vaida");
            wordsResult.ElementAt(1).Text.ShouldBe("Vidas");
            wordsResult.ElementAt(2).Text.ShouldBe("Sabal");
        }
    }
}
