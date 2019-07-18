
namespace Contracts.DTO
{
    public class CachedWord
    {
        public int Id { get; set; }
        public int PhraseId { get; set; }
        public int AnagramId { get; set; }
        public Anagram Anagram { get; set; }
        public Phrase Phrase { get; set; }
    }
}
