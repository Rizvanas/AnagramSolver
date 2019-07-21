using Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("anagrams")]
    public class AnagramsController : ControllerBase
    {
        private readonly IAnagramsService _anagramsService;

        public AnagramsController(IAnagramsService anagramsService)
        {
            _anagramsService = anagramsService;
        }
        
        [HttpGet]
        public ActionResult<string> GetAnagrams([FromHeader]string word)
        {
            if (word == null)
                return BadRequest();

            var IpAdress = HttpContext.Connection.RemoteIpAddress.ToString();
            return Ok(_anagramsService.GetAnagrams(word, IpAdress));
        }
    }
}

