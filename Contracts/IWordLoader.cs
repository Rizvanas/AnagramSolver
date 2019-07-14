using Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Contracts
{
    public interface IWordLoader
    {
        IEnumerable<Word> LoadFromFile(string filePath);
    }
}
