using Contracts.DTO;
using Contracts.Models;
using Contracts.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.WebApi.Controllers
{
    [EnableCors("AllowAnyOriginPolicy")]
    [Route("api/words")]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpPost]
        public ActionResult<IList<Word>> GetWords([FromBody] PaginationFilter filter)
        {
            var cookie = Request.Cookies["CurrentPage"];
            filter.Page = (!String.IsNullOrEmpty(cookie) && filter.Page == null)
                ? Convert.ToInt32(cookie)
                : filter.Page;

            SetPagingCookie(filter.Page);
            return Ok(new { words = _wordsService.GetWords(filter.Page, filter.PageSize) });
        }

        [HttpPost("search")]
        public ActionResult<IList<Word>> GetSearchedWords([FromBody] Phrase phrase)
        {
            if (String.IsNullOrWhiteSpace(phrase.Text))
                return BadRequest(new { errorMessage = "Search phrase is required" } );

            return Ok(new { Words = _wordsService.GetWords(phrase.Text).ToList() });
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
