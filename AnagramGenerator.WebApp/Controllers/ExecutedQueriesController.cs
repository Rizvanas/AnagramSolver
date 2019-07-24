using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("queries")]
    public class ExecutedQueriesController : Controller
    {
        private readonly IUserLogsService _userLogsService;

        public ExecutedQueriesController(IUserLogsService userLogsService)
        {
            _userLogsService = userLogsService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(new ExecutedQueryViewModel
            {
                UserLogs = _userLogsService.GetUserLogs().ToList()
            });
        }
    }
}
