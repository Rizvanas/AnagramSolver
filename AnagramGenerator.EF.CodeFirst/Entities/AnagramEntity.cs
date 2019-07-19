using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class AnagramEntity
    {
        public int Id { get; set; }
        public string Anagram { get; set; }
        public List<CachedWordEntity> CachedWords { get; set; }
    }
}
