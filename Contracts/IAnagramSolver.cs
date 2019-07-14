using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAnagramSolver
    {
        IEnumerable<Word> GetAnagrams(string myWords, string IpAddress);
    }
}
