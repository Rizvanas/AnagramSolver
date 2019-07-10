using Core.Domain;
using Core.DTO;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IFileWordRepository : IWordRepository
    {
        IEnumerable<Word> GetPaginizedWords(PaginationFilter filter);
        IEnumerable<string> GetWordsText();
    }
}
