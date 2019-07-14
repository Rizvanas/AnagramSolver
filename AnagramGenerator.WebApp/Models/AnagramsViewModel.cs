using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramsViewModel
    {
        public string InputWords { get; set; }
        public List<Word> Anagrams { get; set; }
    }
}
