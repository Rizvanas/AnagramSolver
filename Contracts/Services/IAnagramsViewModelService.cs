using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IAnagramsViewModelService
    {
        List<Anagram> GetAnagrams(string phrase, string ip);
    }
}
