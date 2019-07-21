using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Entities
{
    public partial class UserWordEntity
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int UserId { get; set; }

        public virtual UserEntity User { get; set; }
    }
}
