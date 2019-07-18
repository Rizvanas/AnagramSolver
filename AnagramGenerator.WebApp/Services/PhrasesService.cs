using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.WebApp.Services
{
    public class PhrasesService : IPhrasesService
    {
        private readonly IPhrasesRepository _phrasesRepository;

        public PhrasesService(IPhrasesRepository phrasesRepository)
        {
            _phrasesRepository = phrasesRepository;
        }

        public Phrase GetPhrase(string word)
        {
            return _phrasesRepository
                .GetPhrases()
                .FirstOrDefault(p => p.Text == word);
        }
    }
}
