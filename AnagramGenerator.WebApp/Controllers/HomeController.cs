using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;

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
            var IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            List<string> anagrams = new List<string>();

            if(words != null)
                anagrams =  _anagramSolver.GetAnagrams(words, IpAddress);

            return View(
                new AnagramsViewModel
                {
                    InputWords = words,
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

            return View(
                 "Index",
                 new AnagramsViewModel
                 {
                     InputWords = words,
                     Anagrams = anagrams
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
