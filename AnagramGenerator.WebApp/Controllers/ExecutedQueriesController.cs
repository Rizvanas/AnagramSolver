using AnagramGenerator.WebApp.Models;
using Contracts;
using Contracts.Repositories;
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
        private readonly IUserLogsRepository _userLogsRepository;

        public ExecutedQueriesController(IUserLogsRepository userLogRepository)
        {
            _userLogsRepository = userLogRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userLogs = _userLogsRepository.GetUserLogs();
            return View(new ExecutedQueryViewModel
            {
                UserLogs = userLogs.ToList()
            });
        }
    }
}
