using Interfaces;
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

            var IpAdress = HttpContext.Connection.RemoteIpAddress.ToString();
            return Ok(_anagramSolver.GetAnagrams(word, IpAdress));
        }
    }
}

