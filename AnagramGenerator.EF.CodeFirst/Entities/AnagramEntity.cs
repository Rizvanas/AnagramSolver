﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public class AnagramEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Anagram { get; set; }
        public List<CachedWordEntity> CachedWords { get; set; }
    }
}
