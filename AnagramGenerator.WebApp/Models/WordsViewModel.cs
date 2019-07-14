using Core.Domain;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Models
{
    public class WordsViewModel
    {
        public List<Word> Words { get; set; }
        public int? Page { get; set; }
    }
}
