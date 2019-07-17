using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Contracts.Services;
using Contracts.Extensions;

namespace AnagramGenerator.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordsService _wordsService;
        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        [HttpGet("words")]
        public IActionResult Index(int? page, int pageSize)
        {
            var cookie = Request.Cookies["CurrentPage"];
            if (cookie != null)
                SetPagingCookie(page);

            page = (cookie != null && !String.IsNullOrEmpty(cookie) && page == null)
                ? Convert.ToInt32(cookie)
                : page;

            return View(new WordsViewModel
            {
                Words = _wordsService.GetWords(page, pageSize).ToList(),
                Page = page
            });
        }

        [HttpGet("words/update")]
        public IActionResult Update()
        {
            return View(new WordsUpdateViewModel
            {
                GotUpdated = true,
                Word = null
            });
        }

        [HttpPost("words/update")]
        public IActionResult UpdateList(string word)
        {
            _wordsService.AddWord(word);
            return View("Update", new WordsUpdateViewModel
            {
                GotUpdated = true,
                Word = word.ToWordModel()
            });
        }

        [HttpPost("words/search")]
        public IActionResult Search(string searchPhrase)
        {
            var words = _wordsService.GetWords(searchPhrase).ToList();
            return View("Index", new WordsViewModel { Words = words });
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
