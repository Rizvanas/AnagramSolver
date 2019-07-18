
namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public partial class CachedWordEntity
    {
        public int PhraseId { get; set; }
        public int AnagramId { get; set; }
        public int Id { get; set; }

        public virtual AnagramEntity Anagram { get; set; }
        public virtual PhraseEntity Phrase { get; set; }
    }
}
