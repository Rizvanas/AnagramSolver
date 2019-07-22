using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public partial class WordEntity
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
