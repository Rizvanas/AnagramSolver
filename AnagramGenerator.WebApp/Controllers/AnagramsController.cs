using Core.Domain;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.Design.OperationExecutor;

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

            return Ok(_anagramSolver.GetStringAnagrams(word));
        }
    }
}

