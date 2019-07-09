using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAnagramWebAppClient
    {
        Task<List<string>> GetAnagramsAsync (string word);
    }
}
