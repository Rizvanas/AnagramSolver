using AnagramGenerator.WebApp.Controllers;
using Contracts.DTO;
using Contracts.Models;
using Contracts.Repositories;
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

            _anagramsService.Received(1).GetAnagrams(testWord, testIp);

            result.ShouldBeOfType<ViewResult>();
            result.ViewData.Model.ShouldBeOfType<AnagramsViewModel>();

            var anagramsViewModel = result.Model as AnagramsViewModel;

            anagramsViewModel.Anagrams.ShouldNotBeNull();
            anagramsViewModel.Anagrams.Count.ShouldNotBeNull();
            anagramsViewModel.Anagrams.Count.ShouldBe(3);

            anagramsViewModel.Anagrams.First().ShouldNotBeNull();
            anagramsViewModel.Anagrams.First().ShouldBeOfType<Anagram>();
            anagramsViewModel.Anagrams.First().Text.ShouldBe("deivas");

            anagramsViewModel.Phrase.ShouldNotBeNull();
            anagramsViewModel.Phrase.Text.ShouldBe(testWord);

            anagramsViewModel.ErrorMessage.ShouldBeNull();
        }

        [Test]
        public void UpdateReturnsAnagramsViewModel_WithErrorMessage()
        {
            string testWord = "Testas";
            var testIp = _homeController.HttpContext.Connection.RemoteIpAddress.ToString();
            var testErrorMessage = "You exceeded free searches limit, please add new word";

            _anagramsService
                .GetAnagrams(Arg.Is(testWord), Arg.Is(testIp))
                .Returns(x => { throw new Exception(testErrorMessage); });
            
            var result = _homeController.Update(testWord) as ViewResult;

            _anagramsService.Received(1).GetAnagrams(testWord, testIp);

            result.ShouldBeOfType<ViewResult>();
            result.ViewData.Model.ShouldBeOfType<AnagramsViewModel>();

            var anagramsViewModel = result.Model as AnagramsViewModel;
            anagramsViewModel.Anagrams.ShouldBeNull();
            anagramsViewModel.Phrase.ShouldBeNull();
            anagramsViewModel.ErrorMessage.ShouldNotBeNull();
            anagramsViewModel.ErrorMessage.ShouldBe(testErrorMessage);
        }

        [Test]
        public void UpdateReturnsNoContent()
        {
            string testWord = null;
            var testIp = _homeController.HttpContext.Connection.RemoteIpAddress.ToString();

            _anagramsService
                .GetAnagrams(Arg.Is(testWord), Arg.Is(testIp))
                .Returns(GetTestAnagrams());

            var result = _homeController.Update(testWord) as NoContentResult;

            _anagramsService.Received(0).GetAnagrams(testWord, testIp);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<NoContentResult>();
        }

        [Test]
        public void UpdateReturnsAnagramsViewModel_WithAnagramsAndPhrase()
        {
            string testWord = "Viedas";
            var testIp = _homeController.HttpContext.Connection.RemoteIpAddress.ToString();

            _anagramsService
                .GetAnagrams(Arg.Is(testWord), Arg.Is(testIp))
                .Returns(GetTestAnagrams());

            var result = _homeController.Update(testWord) as ViewResult;

            result.ShouldBeOfType<ViewResult>();
            result.ViewData.Model.ShouldBeOfType<AnagramsViewModel>();

            var anagramsViewModel = result.Model as AnagramsViewModel;

            _anagramsService.Received(1).GetAnagrams(testWord, testIp);

            anagramsViewModel.Anagrams.ShouldNotBeNull();
            anagramsViewModel.Anagrams.Count.ShouldNotBeNull();
            anagramsViewModel.Anagrams.Count.ShouldBe(3);

            anagramsViewModel.Anagrams.First().ShouldNotBeNull();
            anagramsViewModel.Anagrams.First().ShouldBeOfType<Anagram>();
            anagramsViewModel.Anagrams.First().Text.ShouldBe("deivas");

            anagramsViewModel.Phrase.ShouldNotBeNull();
            anagramsViewModel.Phrase.Text.ShouldBe(testWord);

            anagramsViewModel.ErrorMessage.ShouldBeNull();
        }


        private List<Anagram> GetTestAnagrams()
        {
            return new List<Anagram>
            {
                new Anagram {Id = 1, Text = "deivas"},
                new Anagram {Id = 2, Text = "veidas"},
                new Anagram {Id = 3, Text = "dievas"}
            };
        }
    }
}
