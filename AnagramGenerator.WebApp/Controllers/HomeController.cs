using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Contracts;
using Microsoft.AspNetCore.Http;
using Contracts.Entities;
using Contracts.Repositories;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IPhrasesRepository _phrasesRepository;

        public HomeController(IAnagramSolver anagramSolver, IPhrasesRepository phrasesRepository)
        {
            _anagramSolver = anagramSolver;
            _phrasesRepository = phrasesRepository;
        }

        [HttpGet("{words?}")]
        public IActionResult Index(string words)
        {
            var IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var anagrams = new List<AnagramEntity>();

            if(words != null)
                anagrams = _anagramSolver.GetAnagrams(words, IpAddress).ToList();

            return View(
                new AnagramsViewModel
                {
                    InputWords = _phrasesRepository.GetPhrase(words),
                    Anagrams = anagrams
                });
        }

        [HttpPost("/")]
        public IActionResult Update(string words)
        {
            var IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (words == null)
                return NoContent();
            var anagrams = _anagramSolver.GetAnagrams(words, IpAddress);

            var phrase = _phrasesRepository.GetPhrase(words);
            return View(
                 "Index",
                 new AnagramsViewModel
                 {
                     InputWords = phrase,
                     Anagrams = anagrams.ToList()
                 });
        }

        [HttpGet("cookie")]
        public IActionResult DisplayCookieInfo()
        {
            var cookies = HttpContext.Request.Cookies;
            return View(new CookieInfoViewModel { Cookies = cookies });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetSearchCookie(string word)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(15);
            Response.Cookies.Append("CurrentSearchWord", word, option);
        }
    }
}
