using AnagramGenerator.WebApp.Models;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("queries")]
    public class ExecutedQueriesController : Controller
    {
        private readonly IUserLogRepository _userLogRepository;
        private readonly ISqlWordRepository _sqlWordReposiotry;

        public ExecutedQueriesController(IUserLogRepository userLogRepository, ISqlWordRepository sqlWordRepository)
        {
            _userLogRepository = userLogRepository;
            _sqlWordReposiotry = sqlWordRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userLogs = _userLogRepository.GetUserLogs();
            return View(new ExecutedQueryViewModel
            {
                UserLogs = userLogs
            });
        }
    }
}
