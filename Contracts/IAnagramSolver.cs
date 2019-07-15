using Contracts.Entities;
using System.Collections.Generic;

namespace Contracts
{
    public interface IAnagramSolver
    {
        IEnumerable<AnagramEntity> GetAnagrams(PhraseEntity phrase, string IpAddress);
    }
}
