﻿
using Contracts.DTO;
using Contracts.Entities;

namespace Contracts.Models
{
    public class WordsUpdateViewModel
    {
        public Word Word { get; set; }
        public bool GotUpdated { get; set; }
    }
}