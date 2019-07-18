using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAnagramWebAppClient
    {
        Task<IList<Word>> GetAnagramsAsync (string word);
    }
}
