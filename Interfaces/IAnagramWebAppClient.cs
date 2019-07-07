using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IAnagramWebAppClient
    {
        List<string> GetAnagrams(string word);
    }
}
