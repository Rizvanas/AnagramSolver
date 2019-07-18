using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        public void AddPhrase(Phrase phrase)
        {
            throw new NotImplementedException();
        }

        public void AddPhrases(params Phrase[] phrases)
        {
            throw new NotImplementedException();
        }

        public void DeletePhrase(int id)
        {
            throw new NotImplementedException();
        }

        public Phrase GetPhrase(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Phrase> GetPhrases()
        {
            throw new NotImplementedException();
        }
    }
}
