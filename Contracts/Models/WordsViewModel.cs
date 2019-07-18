using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Models
{
    public class WordsViewModel
    {
        public List<Word> Words { get; set; }
        public int? Page { get; set; }
    }
}
