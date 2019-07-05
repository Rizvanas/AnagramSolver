using AnagramGenerator.WebApp.Models;
using Core.DTO;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordRepository _wordRepository;
        public WordsController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        [HttpGet("words/{page}/{pageSize}")]
        public IActionResult Index(int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            var filter =  new PaginationFilter { Page = page, PageSize = pageSize };
            var wordsViewModel = new WordsViewModel
            {
                Words = _wordRepository.GetPaginizedWords(filter).ToList()
            };

            return View(wordsViewModel);
        }
    }
}
