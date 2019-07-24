using AnagramGenerator.WebApp.Controllers;
using Contracts.DTO;
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
    public class AnagramsControllerTests
    {
        private IAnagramsService _anagramsService;
        private AnagramsController _anagramsController;

        [SetUp]
        public void Setup()
        {
            _anagramsService = Substitute.For<IAnagramsService>();
            _anagramsController = new AnagramsController(_anagramsService);

            _anagramsController.ControllerContext = new ControllerContext();
            _anagramsController.ControllerContext.HttpContext = new DefaultHttpContext();
            _anagramsController.ControllerContext.HttpContext.Connection.RemoteIpAddress = 
                new IPAddress(new byte[] { 127, 0, 0, 1 });
        }

        [Test]
        public void GetAnagramsReturnsBadRequest_WhenWordIsNullOrWhitespace()
        {
            string testWord = "  ";
            var testIp = _anagramsController.HttpContext.Connection.RemoteIpAddress.ToString();

            _anagramsService
                .GetAnagrams(Arg.Is(testWord), Arg.Is(testIp))
                .Returns(GetTestAnagrams());

            var result = _anagramsController.GetAnagrams(testWord);

            _anagramsService.Received(0).GetAnagrams(testWord, testIp);

            result.ShouldBeOfType<ActionResult<List<string>>>();
            var badRequestResult = result.Result as BadRequestResult;
            badRequestResult.ShouldNotBeNull();
        }

        [Test]
        public void GetAnagramsReturnsOk_WithWordAndIp_WhenWordIsNotNull()
        {
            var testWord = "Ei Svad";
            var testIp = _anagramsController.HttpContext.Connection.RemoteIpAddress.ToString();

            _anagramsService
                .GetAnagrams(Arg.Is(testWord), Arg.Is(testIp))
                .Returns(GetTestAnagrams());

            var result = _anagramsController.GetAnagrams(testWord);
            _anagramsService.Received(1).GetAnagrams(testWord, testIp);

            result.ShouldBeOfType<ActionResult<List<string>>>();

            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<List<Anagram>>();

            var anagrams = okObjectResult.Value as List<Anagram>;
            anagrams.ShouldNotBeNull();
            anagrams.Count.ShouldBe(3);
            anagrams.First().ShouldBeOfType<Anagram>();
            anagrams.First().Text.ShouldBe("deivas");
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
