using Contracts;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        public bool AddPhrase(Phrase phrase)
        {
            throw new NotImplementedException();
        }

        public bool AddPhrases(params Phrase[] phrases)
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
