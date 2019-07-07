using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IAnagramSolver
    {
        List<List<Word>> GetAnagrams(string myWords);
        List<string> GetStringAnagrams(string myWords);
    }
}
