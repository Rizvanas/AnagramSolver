using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IAnagramSolver
    {
        List<Word> GetAnagrams(string myWords, string IpAddress);
    }
}
