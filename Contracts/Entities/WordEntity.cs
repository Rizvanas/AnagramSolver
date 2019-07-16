using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contracts.Entities
{
    public partial class WordEntity
    {
        public int WordId { get; set; }
        public string Word { get; set; }
    }
}
