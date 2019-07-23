using Contracts;
using Contracts.DTO;
using Contracts.Extensions;
using Contracts.Repositories;
using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.WebApp.Services
{
    public class WordsService : IWordsService
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly IAppConfig _appConfig;

        public WordsService(IWordsRepository wordsRepository, IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _wordsRepository = wordsRepository;
        }

        public IList<Word> GetWords(string word)
        {
            return _wordsRepository
                .GetWords()
                .Where(w => w.Text.ToLower().StartsWith(word.ToLower()))
                .ToList();
        }

        public IList<Word> GetWords(int? page, int pageSize)
        {
            var filter = new PaginationFilter
            {
                Page = (page < 1 || page == null) ? 1 : page,
                PageSize = pageSize
            };

            return _wordsRepository
                .GetWords()
                .Skip((filter.Page.Value - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
        }

        public IList<Word> GetWordsForSearch(string phrase)
        {
            var minWordLen = Convert.ToInt32(_appConfig
               .GetConfiguration()
         .GetSection("ConstantValues")["MinWordLength"]);

            return _wordsRepository.GetWords()
                .Where(w => w.Text.Length <= phrase.Length
                         && w.Text.Length >= minWordLen
                         && phrase.GetSearchWord(w.Text) != phrase)
                .OrderByDescending(w => w.Text.Length)
                .ToList();
        }
    }
}
