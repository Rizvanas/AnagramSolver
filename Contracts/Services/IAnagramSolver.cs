using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts.Services
{
    public interface IAnagramSolver
    {
        IList<Anagram> GetAnagrams(string word, User user);
    }
}
