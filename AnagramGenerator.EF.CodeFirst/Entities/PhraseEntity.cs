using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class PhraseEntity
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
        public List<CachedWordEntity> CachedWords { get; set; }
        public List<UserLogEntity> UserLogs { get; set; }
    }
}
