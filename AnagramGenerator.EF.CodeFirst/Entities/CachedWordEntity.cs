
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class CachedWordEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PhraseId { get; set; }
        public int AnagramId { get; set; }
        public AnagramEntity Anagram { get; set; }
        public PhraseEntity Phrase { get; set; }
    }
}
