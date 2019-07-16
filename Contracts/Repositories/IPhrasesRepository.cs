using Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IPhrasesRepository
    {
        IEnumerable<PhraseEntity> GetPhrases();
        PhraseEntity GetPhrase(int id);
        PhraseEntity GetPhrase(string phrase);
        void AddPhrase(PhraseEntity phrase);
    }
}
