using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Contracts.Services;
using Contracts.Extensions;
using Contracts.DTO;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordsService _wordsService;
        private readonly IUserWordsService _userWordsService;

        public WordsController(IWordsService wordsService, IUserWordsService userWordsService)
        {
            _wordsService = wordsService;
            _userWordsService = userWordsService;
        }

        [HttpGet("words")]
        public ViewResult Index(int? page, int pageSize)
        {
            var cookie = Request.Cookies["CurrentPage"];

            page = (!String.IsNullOrEmpty(cookie) && page == null)
                ? Convert.ToInt32(cookie)
                : page;

            SetPagingCookie(page);
            return View(new WordsViewModel
            {
                Words = _wordsService.GetWords(page, pageSize).ToList(),
                Page = page
            });
        }

        [HttpPost("words/search")]
        public IActionResult Search(string searchPhrase)
        {
            var words = new List<Word>();
            try
            {
                words = _wordsService.GetWords(searchPhrase).ToList();
            }
            catch
            {
                return Redirect($"/words?pageSize=100");
            }

            return View("Index", new WordsViewModel { Words = words });
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
