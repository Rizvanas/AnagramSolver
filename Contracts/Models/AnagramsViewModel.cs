using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramsViewModel
    {
        public PhraseEntity InputWords { get; set; }
        public List<AnagramEntity> Anagrams { get; set; }
    }
}
