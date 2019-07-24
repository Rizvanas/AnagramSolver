using AnagramGenerator.WebApp.Controllers;
using Contracts.DTO;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.WebApp.Tests.Controllers
{
    [TestFixture]
    public class ExecutedQueriesControllerTests
    {
        private  IUserLogsService _userLogsService;
        private ExecutedQueriesController _executedQueriesController;

        [SetUp]
        public void Setup()
        {
            _userLogsService = Substitute.For<IUserLogsService>();
            _executedQueriesController = new ExecutedQueriesController(_userLogsService);
        }

        [Test]
        public void IndexReturnsExecutedQueryViewModel_WithUserLogList()
        {
            var userLogsTestList = GetUserLogsTestList();
            _userLogsService.GetUserLogs().Returns(userLogsTestList);

            var viewResult = _executedQueriesController.Index();
            viewResult.ShouldBeOfType<ViewResult>();
            viewResult.ViewData.Model.ShouldBeOfType<ExecutedQueryViewModel>();

            var executedQueryViewModel = viewResult.Model as ExecutedQueryViewModel;
            executedQueryViewModel.UserLogs.Count.ShouldBe(3);
            executedQueryViewModel.UserLogs.First().ShouldBeOfType<UserLog>();
            executedQueryViewModel.UserLogs.First().Anagram.Text.ShouldBe("Karamelengel");
        }

        private List<UserLog> GetUserLogsTestList()
        {
            return new List<UserLog>
            {
                new UserLog
                {
                    Id = 1,
                    SearchTime = 100,
                    Phrase = new Phrase{ Id = 1, Text = "Angela Merkel" },
                    Anagram = new Anagram {Id = 1, Text = "Karamelengel" },
                    User = new User { Id = 1, Ip = "::1", SearchesLeft = 5}
                },
                new UserLog
                {
                    Id = 2,
                    SearchTime = 100,
                    Phrase = new Phrase{ Id = 1, Text = "Angela Merkel" },
                    Anagram = new Anagram {Id = 1, Text = "klare Maengel" },
                    User = new User { Id = 1, Ip = "::1", SearchesLeft = 5}
                },
                new UserLog
                {
                    Id = 3,
                    SearchTime = 100,
                    Phrase = new Phrase{ Id = 1, Text = "Angela Merkel" },
                    Anagram = new Anagram {Id = 1, Text = "gare kamellen" },
                    User = new User { Id = 1, Ip = "::1", SearchesLeft = 5}
                }
            };
        }
    }
}
