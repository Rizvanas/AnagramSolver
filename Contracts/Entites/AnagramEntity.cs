using System;
using System.Collections.Generic;

namespace Contracts.Entities
{
    public partial class AnagramEntity
    {
        public AnagramEntity()
        {
            CachedWords = new HashSet<CachedWordEntity>();
        }

        public int Id { get; set; }
        public string Anagram { get; set; }

        public virtual ICollection<CachedWordEntity> CachedWords { get; set; }
    }
}
