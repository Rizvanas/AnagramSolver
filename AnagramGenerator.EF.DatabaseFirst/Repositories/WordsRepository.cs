using Contracts;
using Contracts.DTO;
using Contracts.Entities;
using Contracts.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class WordsRepository
    {
        private readonly WordsDBContext _wordsDBContext;
        private readonly IAppConfig _appConfig;

        public WordsRepository(WordsDBContext wordsDBContext, IAppConfig appConfig)
        {
            _wordsDBContext = wordsDBContext;
            _appConfig = appConfig;
        }

        public IEnumerable<WordEntity> GetWords()
        {
            return _wordsDBContext.Words;
        }

        public IEnumerable<WordEntity> GetWords(PaginationFilter filter)
        {
            return filter == null
                ? _wordsDBContext.Words

                : _wordsDBContext.Words
                .Skip((filter.Page ?? 1 - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }

        public IEnumerable<WordEntity> GetWords(PhraseEntity phrase)
        {
            return _wordsDBContext.Words
                .Where(w => w.Word.StartsWith(phrase.Phrase));
        }

        public WordEntity GetWord(int id)
        {
            return _wordsDBContext.Words
                .FirstOrDefault(w => w.WordId == id);
        }

        public IEnumerable<WordEntity> GetSearchWords(PhraseEntity phrase)
        {
            var minWordLen = Convert.ToInt32(_appConfig
                .GetConfiguration()
          .GetSection("ConstantValues")["MinWordLength"]);

            return _wordsDBContext.Words
                .Where(w => w.Word.Length <= phrase.Phrase.Length
                         && w.Word.Length >= minWordLen
                         && phrase.Phrase.GetSearchWord(w.Word) != phrase.Phrase)
                .OrderByDescending(w => w.Word.Length);
        }

        public bool AddWord(WordEntity word)
        {
            if (_wordsDBContext.Words.Contains(word))
                return false;

            _wordsDBContext.Words.Add(word);
            return true;
        }
    }
}
