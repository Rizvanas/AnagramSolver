using Core.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IWordLoader
    {
        IEnumerable<Word> Load(string filePath);
    }
}
