using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IAnagramSolver
    {
        List<string> GetAnagrams(string myWords, string IpAddress);
    }
}
