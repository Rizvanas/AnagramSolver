using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class Anagrams
    {
        public Anagrams()
        {
            CachedWords = new HashSet<CachedWords>();
        }

        public int Id { get; set; }
        public string Anagram { get; set; }

        public virtual ICollection<CachedWords> CachedWords { get; set; }
    }
}
