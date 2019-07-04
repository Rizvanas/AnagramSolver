﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IWordRepository
    {
        IEnumerable<Word> GetWords();
    }
}
