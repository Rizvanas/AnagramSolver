using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAnagramWebAppClient
    {
        Task<List<WordEntity>> GetAnagramsAsync (string word);
    }
}
