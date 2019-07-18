using Contracts.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Contracts.Repositories
{
    public interface IPhrasesRepository
    {
        IList<Phrase> GetPhrases();
        Phrase GetPhrase(int id);
        void AddPhrase(Phrase phrase);
        void AddPhrases(params Phrase[] phrases);
        void DeletePhrase(int id);
    }
}
