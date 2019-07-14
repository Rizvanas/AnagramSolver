using AnagramGenerator.EF.DatabaseFirst.Models;
using AnagramGenerator.EF.DatabaseFirst.Persistence;
using Contracts;
using Core.Domain;
using Core.DTO;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class DbFirstWordRepository : ISqlWordRepository
    {
        private readonly WordsDBContext _wordsDBContext;
        private readonly IAppConfig _appConfig;
        public DbFirstWordRepository(WordsDBContext wordsDBContext, IAppConfig appConfig)
        {
            _wordsDBContext = wordsDBContext;
            _appConfig = appConfig;
        }

        public bool AddCachedWord(Word word, List<Word> anagrams)
        {
            throw new NotImplementedException();
        }
        /* var phraseInsertionQuery = "INSERT INTO Phrases(Phrase) VALUES(@phrase);";
         var cacheInsertionQuery = new StringBuilder()
             .Append("INSERT INTO Anagrams(Anagram) VALUES(@anagram);")
             .Append("INSERT INTO CachedWords(PhraseId, AnagramId)")
             .Append("VALUES((SELECT Id FROM Phrases WHERE Phrase = @phrase),")
             .Append("(SELECT Id From Anagrams WHERE LOWER(REPLACE(Anagram, ' ', '')) = LOWER(REPLACE(@anagram, ' ', ''))));")
             .ToString();*/
        public bool AddWord(Word word)
        {
            _wordsDBContext.Words.Add(new Words { Word = word.Text });
            return true;
        }

        public IEnumerable<Word> GetCachedAnagrams(string phrase)
        {
            return _wordsDBContext.CachedWords.Select(a => new Word { Text = a.Anagram.Anagram });
        }

        public IEnumerable<Word> GetWords(string searchPhrase)
        {
            return _wordsDBContext.Set<Words>()
                .Where(w => w.Word.StartsWith(searchPhrase))
                .Select(w => new Word { Text = w.Word });
        }

        public IEnumerable<Word> GetWords(PaginationFilter filter)
        {
            return filter == null
                ? _wordsDBContext.Set<Words>()
                    .Select(w => new Word { Text = w.Word })

                : _wordsDBContext.Set<Words>()
                    .Skip((filter.Page ?? 1 - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .Select(w => new Word { Text = w.Word });
        }

        public IEnumerable<Word> SearchWords(string phrase)
        {
            var minWordLen = Convert.ToInt32(_appConfig
                .GetConfiguration()
                .GetSection("ConstantValues")["MinWordLength"]);

            return _wordsDBContext.Set<Words>()
                .Where(w => w.Word.Length <= phrase.Length
                         && w.Word.Length >= minWordLen
                         && phrase.GetSearchWord(w.Word) != phrase)
                .OrderByDescending(w => w.Word.Length)
                .Select(w => new Word { Text = w.Word });
        }
    }
}

