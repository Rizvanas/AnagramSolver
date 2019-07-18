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
        public WordsService(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
        }

        public IList<Word> GetWords(string word)
        {
            return _wordsRepository
                .GetWords()
                .Where(w => w.Text.StartsWith(word))
                .ToList();
        }

        public IList<Word> GetWords(int? page, int pageSize)
        {
            var filter = new PaginationFilter { Page = (page < 1) ? 1 : page, PageSize = pageSize };

            return _wordsRepository
                .GetWords()
                .Skip((filter.Page ?? 1 - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();
        }

        public bool AddWord(string word)
        {
            return _wordsRepository.AddWord(new Word { Text = word });
        }
    }
}
