using Core.Domain;
using Core.DTO;
using System.Collections.Generic;

namespace Contracts
{
    public interface IFileWordRepository : IWordRepository
    {
        IEnumerable<Word> GetPaginizedWords(PaginationFilter filter);
        IEnumerable<string> GetWordsText();
    }
}
