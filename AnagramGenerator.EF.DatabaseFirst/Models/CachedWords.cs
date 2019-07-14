using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class CachedWords
    {
        public int PhraseId { get; set; }
        public int AnagramId { get; set; }
        public int Id { get; set; }

        public virtual Anagrams Anagram { get; set; }
        public virtual Phrases Phrase { get; set; }
    }
}
