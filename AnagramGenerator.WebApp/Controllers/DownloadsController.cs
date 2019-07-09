using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Controllers
{
    [Route("download")]
    public class DownloadsController : ControllerBase
    {
        [HttpGet("dictionary")]
        public async Task<IActionResult> GetDictionaryAsync()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "zodynas.txt");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
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
            return new Dictionary<string, string> { { ".txt", "text/plain" } };
        }
    }
}
