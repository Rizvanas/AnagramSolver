using Contracts;
using Contracts.Entities;
using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("anagrams")]
    public class AnagramsController : ControllerBase
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly IPhrasesRepository _phrasesRepository;

        public AnagramsController(IAnagramSolver anagramSolver , IPhrasesRepository phrasesRepository)
        {
            _anagramSolver = anagramSolver;
            _phrasesRepository = phrasesRepository;
        }
        
        [HttpGet]
        public ActionResult<string> GetAnagrams([FromHeader]string word)
        {
            if (word == null)
                return BadRequest();

            var IpAdress = HttpContext.Connection.RemoteIpAddress.ToString();

            return Ok(_anagramSolver.GetAnagrams(word, IpAdress));
        }
    }
}

