
using Contracts.Entities;

namespace AnagramGenerator.WebApp.Models
{
    public class WordsUpdateViewModel
    {
        public WordEntity Word { get; set; }
        public bool GotUpdated { get; set; }
    }
}
