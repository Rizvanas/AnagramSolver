using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.WebApi.Controllers
{

    [Route("api/userWords")]
    [EnableCors("AllowAnyOriginPolicy")]
    [ApiController]
    public class UserWordsController : ControllerBase
    {
        private readonly IUserWordsService _userWordsService;

        public UserWordsController(IUserWordsService userWordsService)
        {
            _userWordsService = userWordsService;
        }

        [HttpPost]
        public ActionResult<IList<UserWord>> GetUserWords([FromBody] PaginationFilter filter)
        {
            var cookie = Request.Cookies["CurrentPage"];
            filter.Page = (!String.IsNullOrEmpty(cookie) && filter.Page == null)
                ? Convert.ToInt32(cookie)
                : filter.Page;

            SetPagingCookie(filter.Page);
            return Ok(new { userWors = _userWordsService.GetUserWords(filter.Page, filter.PageSize) });
        }

        [HttpPost("search")]
        public ActionResult<IList<UserWord>> GetSearchedUserWords([FromBody] Phrase phrase)
        {
            if (String.IsNullOrWhiteSpace(phrase.Text))
                return BadRequest(new { errorMessage = "Search phrase is required" });

            return Ok(new { userWors = _userWordsService.GetUserWords(phrase.Text).ToList() });
        }

        [HttpPost("new")]
        public ActionResult AddUserWord([FromBody] UserWord word)
        {
            if (String.IsNullOrWhiteSpace(word.Text))
                return BadRequest(new { errorMessage = "word is required" });

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            _userWordsService.AddUserWord(word.Text, ipAddress);

            return Ok();
        }

        [HttpPut("edit/{id}")]
        public ActionResult UpdateUserWord([FromBody] UserWord word)
        {
            if (word.Id < 0 || String.IsNullOrWhiteSpace(word.Text))
                return NotFound(new { errorMessage = $"word with id of {word.Id} could not be found" });

            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                _userWordsService.UpdateUserWord(word.Id, word.Text, ipAddress);
            }
            catch
            {
                return NotFound(new { errorMessage = $"word with id of {word.Id} could not be found" });
            }

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteUserWord(int id)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                _userWordsService.RemoveUserWord(id, ipAddress);
            }
            catch
            {
                return NotFound(new { errorMessage = $"word with id of {id} could not be found" });
            }

            return Ok();
        }

        [HttpPost]
        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
