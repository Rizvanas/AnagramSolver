using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramGenerator.WebApp.Services
{
    public class PhrasesService : IPhrasesService
    {
        private readonly IPhrasesRepository _phrasesRepository;

        public PhrasesService(IPhrasesRepository phrasesRepository)
        {
            _phrasesRepository = phrasesRepository;
        }

        public IList<Phrase> GetPhrases(string word)
        {

        }
    }
}
