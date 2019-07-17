using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Entities
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
