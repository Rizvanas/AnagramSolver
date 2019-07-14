using Core.Domain;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramsViewModel
    {
        public string InputWords { get; set; }
        public List<Word> Anagrams { get; set; }
    }
}
