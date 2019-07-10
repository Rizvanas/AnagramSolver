using Core.Domain;
using System.Collections.Generic;

namespace Interfaces
{
    public interface ISqlWordRepository : IWordRepository
    {
        bool AddWord(Word word);
        bool AddCachedWord(Word word);
        Word GetCachedWord(string phrase);
    }
}
