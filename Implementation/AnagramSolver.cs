using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Extensions;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections.Specialized;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        private readonly NameValueCollection _appSettings;
        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
            _appSettings = ConfigurationManager.GetSection("ApplicationSettings")
                as NameValueCollection;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            var resultCount = Convert.ToInt32(_appSettings["ResultCount"]);
            myWords = Regex.Replace(myWords, @"\s+", "");
            var words = GetWordsForAnagrams(myWords);

            throw new NotImplementedException();    
        }

        private IEnumerable<IGrouping<int, string>> GetWordsForAnagrams(string myWords)
        {
            var minWordLen = Convert.ToInt32(_appSettings["MinWordLength"]) - 1;
            var searchWord = myWords.GetSearchWord(null);

            return _wordRepository
                    .GetWords()
                    .Where(w => myWords.GetSearchWord(w.Key) != searchWord
                    && w.Key.Length <= searchWord.Length)
                    .Select(w => w.Key)
                    .GroupBy(w => w.Length)
                    .Skip(minWordLen);
        }
    }
}
