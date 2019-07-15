using Contracts.Entities;
using System.Collections.Generic;

namespace Contracts
{
    public interface IAnagramSolver
    {
        IEnumerable<AnagramEntity> GetAnagrams(string myWords, string IpAddress);
    }
}
