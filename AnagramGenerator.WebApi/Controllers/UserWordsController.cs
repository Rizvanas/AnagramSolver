using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.WebApi.Controllers
{

    [Route("api/anagrams")]
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
        public ActionResult<IList<UserWord>> GetUserWords(int? page, int pageSize)
        {
            var cookie = Request.Cookies["CurrentPage"];
            page = (!String.IsNullOrEmpty(cookie) && page == null)
                ? Convert.ToInt32(cookie)
                : page;

            SetPagingCookie(page);
            return Ok(new { userWors = _userWordsService.GetUserWords(page, pageSize) });
        }

        [HttpPost("search")]
        public ActionResult<List<UserWord>> GetSearchedUserWords(string phrase)
        {
            if (String.IsNullOrWhiteSpace(phrase))
                return BadRequest(new { errorMessage = "Search phrase is required" });

            return Ok(new { userWors = _userWordsService.GetUserWords(phrase).ToList() });
        }

        [HttpPost("add")]
        public ActionResult AddUserWord(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
                return BadRequest(new { errorMessage = "word is required" });

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            _userWordsService.AddUserWord(word, ipAddress);

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateUserWord(int id, string word)
        {
            if (id < 0 || String.IsNullOrWhiteSpace(word))
                return NotFound(new { errorMessage = $"word with id {id} could not be found" });

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            _userWordsService.UpdateUserWord(id, word, ipAddress);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteUserWord(int id)
        {

        }

        [HttpPost]
        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
