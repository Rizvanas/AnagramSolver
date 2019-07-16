using Contracts.DTO;
using Contracts.Entities;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Services
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
            return _wordsRepository
                .GetWords(_phrasesRepository.GetPhrase(word))
                .Select(w => new Word { Text = w.Word })
                .ToList();
        }

        public List<Word> GetWords(int? page, int pageSize)
        {
            var filter = new PaginationFilter { Page = (page < 1) ? 1 : page, PageSize = pageSize };

            return _wordsRepository
                .GetWords(filter)
                .Select(w => new Word { Text = w.Word })
                .ToList();
        }

        public void AddWord(string word)
        {
            _wordsRepository.AddWord(new WordEntity { Word = word });
        }
    }
}
