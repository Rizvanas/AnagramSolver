using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAnagramWebAppClient
    {
        Task<List<Word>> GetAnagramsAsync (string word);
    }
}
