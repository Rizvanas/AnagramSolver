﻿using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Entities
{
    public partial class CachedWordEntity
    {
        public int Id { get; set; }
        public int PhraseId { get; set; }
        public int AnagramId { get; set; }

        public virtual AnagramEntity Anagram { get; set; }
        public virtual PhraseEntity Phrase { get; set; }
    }
}
