using Contracts.DTO;
using Contracts.Extensions;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.WebApp.Services
{
    public class WordsService : IWordsService
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly IPhrasesRepository _phrasesRepository;

        public WordsService(IWordsRepository wordsRepository, IPhrasesRepository phrasesRepository)
        {
            _wordsRepository = wordsRepository;
            _phrasesRepository = phrasesRepository;
        }

        public List<Word> GetWords(string word)
        {
            var phrase = _phrasesRepository.GetPhrase(word);

            var words = new List<WordEntity>();
            if (phrase != null)
                words = _wordsRepository.GetWords(phrase).ToList();

            return words.ToWordsList();
        }

        public List<Word> GetWords(int? page, int pageSize)
        {
            var filter = new PaginationFilter { Page = (page < 1) ? 1 : page, PageSize = pageSize };
            var words = _wordsRepository.GetWords(filter);

            return words.ToWordsList();
        }

        public void AddWord(string word)
        {
            _wordsRepository.AddWord(new WordEntity { Word = word });
        }
    }
}
