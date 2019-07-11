using AnagramGenerator.WebApp.Models;
using Core.DTO;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly ISqlWordRepository _sqlWordRepository;
        public WordsController(ISqlWordRepository sqlWordRepository)
        {
            _sqlWordRepository = sqlWordRepository;
        }

        [HttpGet("words")]
        public IActionResult Index(int? page, int pageSize)
        {

            var cookie = Request.Cookies["CurrentPage"];
            PaginationFilter filter = null;

            if (cookie != null && !String.IsNullOrEmpty(cookie) && page == null)
            {
                filter = new PaginationFilter { Page = Convert.ToInt32(cookie), PageSize = pageSize };
                return View(new WordsViewModel
                {
                    Words = _sqlWordRepository.GetWords(filter).ToList(),
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

        private void SetPagingCookie(int? page)
        {
            page = page ?? 1;
            Response.Cookies.Append("CurrentPage", page.ToString());
        }
    }
}
