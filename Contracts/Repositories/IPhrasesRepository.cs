using Contracts.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Contracts.Repositories
{
    public interface IPhrasesRepository
    {
        IList<Phrase> GetPhrases();
        Phrase GetPhrase(int id);
        bool AddPhrase(Phrase phrase);
        bool AddPhrases(params Phrase[] phrases);
        bool DeletePhrase(int id);
    }
}
