using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class Phrases
    {
        public Phrases()
        {
            CachedWords = new HashSet<CachedWords>();
            UserLog = new HashSet<UserLog>();
        }

        public int Id { get; set; }
        public string Phrase { get; set; }

        public virtual ICollection<CachedWords> CachedWords { get; set; }
        public virtual ICollection<UserLog> UserLog { get; set; }
    }
}
