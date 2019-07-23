using AnagramGenerator.WebApp.Services;
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
    public class CachedWordsServiceTests
    {
        private ICachedWordsRepository _cachedWordsRepository;
        private ICachedWordsService _cachedWordsService;

        [SetUp]
        public void Setup()
        {
            _cachedWordsRepository = Substitute.For<ICachedWordsRepository>();
            _cachedWordsService = new CachedWordsService(_cachedWordsRepository);
        }

        [Test]
        public void AddCachedWord_AddsNewCachedWordToRepository()
        {
            var phrase = new Phrase { Id = 1, Text = "Dievas" };
            var anagrams = new List<Anagram>
            {
                new Anagram { Id = 1, Text = "deivas" },
                new Anagram { Id = 2, Text = "veidas" }
            };

            _cachedWordsService.AddCachedWord(phrase, anagrams);
            _cachedWordsRepository.Received().AddCachedWord(Arg.Is<int>(1), Arg.Any<IEnumerable<Anagram>>());
            _cachedWordsRepository.ReceivedCalls().ShouldNotBeEmpty();
            _cachedWordsRepository.ReceivedCalls().ToList().Count.ShouldBe(1);
        }

        [Test]
        public void GetAnagrams_GetsCachedAnagramsBasedOnInputPhrase()
        {
            _cachedWordsRepository.GetCachedWords().Returns(new List<CachedWord>
            {
                new CachedWord
                {
                    Id = 1,
                    AnagramId = 1,
                    PhraseId = 1,
                    Anagram = new Anagram {Id = 1, Text = "rizvanas"},
                    Phrase = new Phrase {Id = 1, Text = "Sanavzir"}
                },

                new CachedWord
                {
                    Id = 2,
                    AnagramId = 2,
                    PhraseId = 1,
                    Anagram = new Anagram {Id = 1, Text = "vanas riz"},
                    Phrase = new Phrase {Id = 1, Text = "Sanavzir"}
                }
            });

            var phrase = new Phrase { Id = 1, Text = "Sanavzir" };
            var anagramsResult = _cachedWordsService.GetAnagrams(phrase);

            _cachedWordsRepository.Received(1).GetCachedWords();
            anagramsResult.Count.ShouldBe(2);
            anagramsResult.First().Text.ShouldBe("rizvanas");
            anagramsResult.Last().Text.ShouldBe("vanas riz");
        }
    }
}
