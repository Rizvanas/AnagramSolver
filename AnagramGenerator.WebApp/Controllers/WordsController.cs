using AnagramGenerator.WebApp.Models;
using Core.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Repositories;
using Contracts.DTO;

namespace AnagramGenerator.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordsRepository _wordsRepository;
        public WordsController(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
        }

        [HttpGet("words")]
        public IActionResult Index(int? page, int pageSize)
        {

            var cookie = Request.Cookies["CurrentPage"];
            PaginationFilter filter = null;

            if (cookie != null && !String.IsNullOrEmpty(cookie) && page == null)
            {
                _wordsRepository
                filter = new PaginationFilter { Page = Convert.ToInt32(cookie), PageSize = pageSize };
                return View(new WordsViewModel
                {
                    Words = _wordsRepository
                    Page = Convert.ToInt32(filter.Page),
                });
            }

            if (page < 1) page = 1;
            filter = new PaginationFilter { Page = page, PageSize = pageSize };
            var wordsViewModel = new WordsViewModel
            {
                Words = _sqlWordRepository.GetWords(filter).ToList(),
                Page = filter.Page
            };

            SetPagingCookie(filter.Page);

            return View(wordsViewModel);
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
            var updated = _sqlWordRepository.AddWord(new Word { Text = word });

            if (updated)
                return new RedirectResult($"/{word}");

            return View("Update", new WordsUpdateViewModel
            {
                GotUpdated = updated,
                Word = word
            });
        }

        [HttpPost("words/search")]
        public IActionResult Search(string searchPhrase)
        {
            var words = _sqlWordRepository.GetWords(searchPhrase);
            return View("Index", new WordsViewModel { Words = words.ToList() });
        }

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
