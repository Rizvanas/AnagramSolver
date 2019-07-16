using AnagramGenerator.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Contracts.DTO;
using Contracts.Services;

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

            if (cookie != null && !String.IsNullOrEmpty(cookie) && page == null)
            {
                var pageFromCookie = Convert.ToInt32(cookie);
                return View(new WordsViewModel
                {
                    Words = _wordsService.GetWords(pageFromCookie, pageSize).ToList(),
                    Page = pageFromCookie
                });
            }

            SetPagingCookie(page);
            return View(new WordsViewModel
            {
                Words = _wordsService.GetWords(page, pageSize, cookie).ToList(),
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
                Word = new Word { Text = word }
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
