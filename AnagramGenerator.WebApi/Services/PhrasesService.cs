using Contracts.DTO;
using Contracts.Repositories;
using Contracts.Services;
using System.Linq;

namespace AnagramGenerator.WebApi.Services
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
                .FirstOrDefault(p => p.Text.Replace(" ", "").ToLower() 
                == word.Replace(" ", "").ToLower());
        }

        public void AddPhrase(string phrase)
        {
            _phrasesRepository.AddPhrase(new Phrase { Text = phrase });
        }
    }
}
