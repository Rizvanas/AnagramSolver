using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class Words
    {
        public int WordId { get; set; }
        public string Word { get; set; }
    }
}
