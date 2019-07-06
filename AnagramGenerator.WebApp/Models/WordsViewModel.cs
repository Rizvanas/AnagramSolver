using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class WordsViewModel
    {
        public List<Word> Words { get; set; }
        public int Page { get; set; }
    }
}
