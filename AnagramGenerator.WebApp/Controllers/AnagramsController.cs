using Contracts;
using Contracts.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("anagrams")]
    public class AnagramsController : ControllerBase
    {
        private readonly IAnagramSolver _anagramSolver;

        public AnagramsController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }
        
        [HttpGet]
        public ActionResult<string> GetAnagrams([FromHeader]string word)
        {
            if (word == null)
                return BadRequest();

            var phrase = new PhraseEntity { Phrase = word };
            var IpAdress = HttpContext.Connection.RemoteIpAddress.ToString();

            return Ok(_anagramSolver.GetAnagrams(phrase, IpAdress));
        }
    }
}

