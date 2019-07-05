using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnagramGenerator.WebApp.Models;
using Interfaces;

namespace AnagramGenerator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;

        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

        [HttpGet("{words}")]
        public IActionResult Index(string words)
        {
            if (words == null)
                return NoContent();

            var anagrams = _anagramSolver
                            .GetAnagrams(words)
                            .Select(a => String.Join(' ', a.Select(t => t.Text)))
                            .ToList();

            return View(
                new AnagramsViewModel
                {
                    InputWords = words,
                    Anagrams = anagrams
                });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
