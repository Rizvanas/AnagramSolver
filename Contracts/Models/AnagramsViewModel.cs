using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Models
{
    public class AnagramsViewModel
    {
        public Phrase Phrase { get; set; }
        public List<Anagram> Anagrams { get; set; }
        public string ErrorMessage { get; set; }
    }
}
