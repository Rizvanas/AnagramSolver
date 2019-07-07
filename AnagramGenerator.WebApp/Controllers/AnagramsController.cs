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

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromHeader] string filename)
        {
            if (filename == null)
                return BadRequest();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
            var memory = new MemoryStream();
            using(var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
           {
               {".txt", "text/plain"},
               {".pdf", "application/pdf"},
               {".doc", "application/vnd.ms-word"},
               {".docx", "application/vnd.ms-word"},
               {".xls", "application/vnd.ms-excel"},
               {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
               {".png", "image/png"},
               {".jpg", "image/jpeg"},
               {".jpeg", "image/jpeg"},
               {".gif", "image/gif"},
               {".csv", "text/csv"}
           };
        }

    }
}

