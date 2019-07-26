using Contracts.DTO;
using Contracts.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApi.Controllers
{
    [Route("api/anagrams")]
    [EnableCors("AllowAnyOriginPolicy")]
    [ApiController]
    public class AnagramsController : ControllerBase
    {
        private readonly IAnagramsService _anagramsService;

        public AnagramsController(IAnagramsService anagramsService)
        {
            _anagramsService = anagramsService;
        }

        [HttpPost]
        public ActionResult<IList<Anagram>> GetAnagrams(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
                return BadRequest(new { errorMessage = "Word is required" });

            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            return Ok(new { anagrams = _anagramsService.GetAnagrams(word, ipAddress) });
        }
    }
}
