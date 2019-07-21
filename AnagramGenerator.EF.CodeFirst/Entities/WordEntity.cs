
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class WordEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WordId { get; set; }

        public string Word { get; set; }
    }
}
