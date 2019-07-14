using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class Phrases
    {
        public int Id { get; set; }
        public string Phrase { get; set; }
    }
}
