using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnagramGenerator.WebApi.Controllers
{
    [Route("api/userLogs")]
    [EnableCors("AllowAnyOriginPolicy")]
    [ApiController]
    public class UserLogsController : ControllerBase
    {
        private readonly IUserLogsService _userLogsService;

        public UserLogsController(IUserLogsService userLogsService)
        {
            _userLogsService = userLogsService;
        }

        public ActionResult<IList<UserLog>> GetUserLogs()
        {
            return Ok(new { userLogs = _userLogsService.GetUserLogs() });
        }
    }
}
