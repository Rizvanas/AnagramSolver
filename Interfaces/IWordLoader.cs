using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Interfaces
{
    public interface IWordLoader
    {
        Dictionary<string, string> Load(string filePath);
    }
}
