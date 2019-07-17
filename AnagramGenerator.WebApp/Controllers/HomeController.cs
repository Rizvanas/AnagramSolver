using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Contracts.Models;
using Contracts;
using Contracts.Extensions;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;

        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

        [HttpGet("{words?}")]
        public IActionResult Index(string words)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            return View(new AnagramsViewModel
            {
                Anagrams = _anagramSolver.GetAnagrams(words, ipAddress).ToAnagramsList(),
                Phrase = words.ToPhraseModel() 
            });; 
        }

        [HttpPost("/")]
        public IActionResult Update(string words)
        {
            if (words == null)
                return NoContent();

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            return View(
                "Index",
                new AnagramsViewModel
                {
                    Anagrams = _anagramSolver.GetAnagrams(words, ipAddress).ToAnagramsList(),
                    Phrase = words.ToPhraseModel()
                });
                
        }

        [HttpGet("cookie")]
        public IActionResult DisplayCookieInfo()
        {
            return View(new CookieInfoViewModel { Cookies = HttpContext.Request.Cookies });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetSearchCookie(string word)
        {
            var option = new CookieOptions { Expires = DateTime.Now.AddMinutes(15) }; 
            Response.Cookies.Append("CurrentSearchWord", word, option);
        }
    }
}
