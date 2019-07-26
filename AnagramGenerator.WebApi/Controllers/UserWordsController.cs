using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return Ok(new { words = _userWordsService.GetUserWords(page, pageSize) });
        }

        [HttpPost("search")]
        public ActionResult<List<Word>> GetSearchedWords(string phrase)
        {
            if (String.IsNullOrWhiteSpace(phrase))
                return BadRequest(new { errorMessage = "Search phrase is required" });

            var words = _userWordsService.GetUserWords(phrase).ToList();
            return Ok(new { words = words.ToList() });
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
