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
    public class AnagramsControllerTests
    {
        private IAnagramsService _anagramsService;
        private AnagramsController _anagramsController;

        [SetUp]
        public void Setup()
        {
            _anagramsService = Substitute.For<IAnagramsService>();
        }

        [Test]
        public void GetAnagramsReturnsBadRequest_WhenWordIsNull()
        {

        }

        [Test]
        public void GetAnagramsReturnsOk_WithWordAndIp_WhenWordIsNotNull()
        {

        }
    }
}
