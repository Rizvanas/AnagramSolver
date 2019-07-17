using Contracts.DTO;
using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IPhrasesRepository
    {
        IEnumerable<Phrase> GetPhrases();
        Phrase GetPhrase(int id);
        Phrase GetPhrase(string phrase);
        void AddPhrase(Phrase phrase);
    }
}
