﻿using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Services
{
    public interface IAnagramsService
    {
        IList<Anagram> GetAnagrams(string word, string ipAddress);
    }
}
