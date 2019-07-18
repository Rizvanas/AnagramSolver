using Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IPhrasesService
    {
        IList<Phrase> GetPhrases(string word);
    }
}
