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
    public class PhrasesServiceTests
    {
        private IPhrasesRepository _phrasesRepository;
        private IPhrasesService _phrasesService;

        [SetUp]
        public void Setup()
        {
            _phrasesRepository = Substitute.For<IPhrasesRepository>();
            _phrasesService = new PhrasesService(_phrasesRepository);
        }

        [Test]
        public void GetPhrase_ReturnsPhraseBasedOnStringInput()
        {
            _phrasesRepository.GetPhrases().Returns(new List<Phrase>
            {
                new Phrase { Id = 1, Text = "Labas" },
                new Phrase { Id = 2, Text = "Vakras" },
                new Phrase { Id = 3, Text = "Rizvanai" },
                new Phrase { Id = 4, Text = "Chalilovai" }
            });


            var phraseResult = _phrasesService.GetPhrase("rizvanai");
            phraseResult.ShouldNotBeNull();
            phraseResult.ShouldBeOfType<Phrase>();
            phraseResult.Id.ShouldBe(3);
            phraseResult.Text.ShouldBe("Rizvanai");
        }

        [Test]
        public void AddPhrase_AddsNewPhraseToPhrasesRepository()
        {
            _phrasesService.AddPhrase("Labas");

            _phrasesRepository.Received().AddPhrase(Arg.Any<Phrase>());
            _phrasesRepository.ReceivedCalls().ShouldNotBeEmpty();
            _phrasesRepository.ReceivedCalls().ToList().Count.ShouldBe(1);
        }
    }
}
