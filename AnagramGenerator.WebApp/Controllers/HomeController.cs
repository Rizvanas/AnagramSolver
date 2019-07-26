using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Contracts.Models;
using Contracts.Extensions;
using System.Linq;
using Contracts.Services;
using Contracts.DTO;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramsService _anagramsService;

        public HomeController(IAnagramsService anagramsService)
        {
            _anagramsService = anagramsService;
        }


        [HttpGet("{words?}")]
        public IActionResult Index(string words)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var anagrams = new List<Anagram>();
            try
            {
                anagrams = _anagramsService.GetAnagrams(words, ipAddress).ToList();
            }
            catch (Exception e)
            {
                return View("Index",
                    new AnagramsViewModel
                    {
                        ErrorMessage = e.Message
                    });
            }

            return View(new AnagramsViewModel
            {
                ErrorMessage = null,
                Anagrams = anagrams,
                Phrase = words.ToPhraseModel() 
            });
        }

        [HttpPost("/")]
        public IActionResult Update(string words)
        {
            if (words == null)
                return NoContent();

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            var anagrams = new List<Anagram>();

            try
            {
                anagrams = _anagramsService.GetAnagrams(words, ipAddress).ToList();
            }
            catch (Exception e)
            {
                return View("Index",
                    new AnagramsViewModel
                    {
                        ErrorMessage = e.Message
                    });
            }

            return View(
                "Index",
                new AnagramsViewModel
                {
                    Anagrams = anagrams,
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
    }
}
