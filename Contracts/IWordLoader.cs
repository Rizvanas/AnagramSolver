using Contracts.Entities;
using System.Collections.Generic;

namespace Contracts
{
    public interface IWordLoader
    {
        IEnumerable<WordEntity> LoadFromFile(string filePath);
    }
}
