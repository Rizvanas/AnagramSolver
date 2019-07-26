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
    public class WordsController : ControllerBase
    {
        private readonly IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpPost]
        public ActionResult<List<Word>> GetWords(int? page, int pageSize)
        {
            var cookie = Request.Cookies["CurrentPage"];
            page = (!String.IsNullOrEmpty(cookie) && page == null)
                ? Convert.ToInt32(cookie)
                : page;

            SetPagingCookie(page);
            return Ok(new { words = _wordsService.GetWords(page, pageSize) });
        }

        [HttpPost("search")]
        public ActionResult<List<Word>> GetSearchedWords(string phrase)
        {
            if (String.IsNullOrWhiteSpace(phrase))
                return BadRequest(new { errorMessage = "Search phrase is required" } );

            return Ok(new { Words = _wordsService.GetWords(phrase).ToList() });
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
