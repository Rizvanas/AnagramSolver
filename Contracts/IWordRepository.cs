using Core.Domain;
using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts
{
    public interface IWordRepository
    {
        IEnumerable<Word> GetWords(PaginationFilter filter);
        IEnumerable<Word> SearchWords(string phrase);
    }
}
