﻿using Contracts.Entities;
using Core.Domain;
using System.Collections.Generic;

namespace AnagramGenerator.WebApp.Models
{
    public class AnagramsViewModel
    {
        public PhraseEntity InputWords { get; set; }
        public List<AnagramEntity> Anagrams { get; set; }
    }
}
