using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class PhraseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Phrase { get; set; }
        public List<CachedWordEntity> CachedWords { get; set; }
        public List<UserLogEntity> UserLogs { get; set; }
    }
}
