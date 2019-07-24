using AnagramGenerator.WebApp.Controllers;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private IAnagramsService _anagramsService;
        private HomeController homeController;

        [SetUp]
        public void Setup()
        {
            _anagramsService = Substitute.For<IAnagramsService>();
            homeController = new HomeController(_anagramsService);
        }

        [Test]
        public void IndexReturnsAnagramsViewModel_WithAllData()
        {

        }

        [Test]
        public void UpdateReturnsAnagramsViewModel_WithErrorMessage()
        {

        }

        [Test]
        public void UpdateReturnsAnagramsViewModel_WithAnagramsAndPhrase()
        {

        }
    }
}
