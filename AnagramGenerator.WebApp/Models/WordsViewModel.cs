using Contracts.Entities;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Models
{
    public class WordsViewModel
    {
        public List<WordEntity> Words { get; set; }
        public int? Page { get; set; }
    }
}
