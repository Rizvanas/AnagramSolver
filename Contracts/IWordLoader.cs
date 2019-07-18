using Contracts.DTO;
using System.Collections.Generic;

namespace Contracts
{
    public interface IWordLoader
    {
        IList<Word> LoadFromFile(string filePath);
    }
}
