using Contracts.DTO;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        public void AddPhrase(PhraseEntity phrase)
        {
            throw new NotImplementedException();
        }

        public Phrase GetPhrase(int id)
        {
            throw new NotImplementedException();
        }

        public Phrase GetPhrase(string phrase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Phrase> GetPhrases()
        {
            throw new NotImplementedException();
        }
    }
}
