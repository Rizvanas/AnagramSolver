using AnagramGenerator.WebApp.Controllers;
using Contracts.DTO;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private IAnagramsService _anagramsService;
        private HomeController _homeController;

        [SetUp]
        public void Setup()
        {
            _anagramsService = Substitute.For<IAnagramsService>();
            _homeController = new HomeController(_anagramsService);

            _homeController.ControllerContext = new ControllerContext();
            _homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            _homeController.ControllerContext.HttpContext.Connection.RemoteIpAddress =
                new IPAddress(new byte[] { 127, 0, 0, 1 });
        }

        [Test]
        public void IndexReturnsAnagramsViewModel_WithAllData()
        {
            string testWord = "Viedas";
            var testIp = _homeController.HttpContext.Connection.RemoteIpAddress.ToString();

            _anagramsService
                .GetAnagrams(Arg.Is(testWord), Arg.Is(testIp))
                .Returns(GetTestAnagrams());

            var result = _homeController.Index(testWord) as ViewResult;

            result.ShouldBeOfType<ViewResult>();
            result.ViewData.Model.ShouldBeOfType<AnagramsViewModel>();

            var anagramsViewModel = result.Model as AnagramsViewModel;
            anagramsViewModel.Anagrams.Count.ShouldNotBeNull();
            anagramsViewModel.Anagrams.Count.ShouldBe(3);

            anagramsViewModel.Anagrams.First().ShouldNotBeNull();
            anagramsViewModel.Anagrams.First().ShouldBeOfType<Anagram>();
            anagramsViewModel.Anagrams.First().Text.ShouldBe("deivas");

            anagramsViewModel.ErrorMessage.ShouldBeNull();
            anagramsViewModel.Phrase.ShouldNotBeNull();
            anagramsViewModel.Phrase.Text.ShouldBe(testWord);
        }

        [Test]
        public void UpdateReturnsAnagramsViewModel_WithErrorMessage()
        {

        }

        [Test]
        public void UpdateReturnsAnagramsViewModel_WithAnagramsAndPhrase()
        {

        }

        private List<Anagram> GetTestAnagrams()
        {
            return new List<Anagram>
            {
                new Anagram {Id = 1, Text = "deivas"},
                new Anagram {Id = 1, Text = "veidas"},
                new Anagram {Id = 1, Text = "dievas"}
            };
        }
    }
}
