using Contracts;
using Contracts.Extensions;
using Contracts.Repositories;
using Contracts.Services;
using System.Linq;

namespace DatabaseSeeder
{
    public class WordsSeeder : ISeeder
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly IWordLoader _wordLoader;
        private readonly IAppConfig _appConfig;

        public WordsSeeder(IWordsRepository wordsRepository, IWordLoader wordLoader, IAppConfig appConfig)
        {
            _wordsRepository = wordsRepository;
            _wordLoader = wordLoader;
            _appConfig = appConfig; 
        }
        
        public void Seed()
        {
            var dictionaryFilePath = _appConfig
                .GetConfiguration()["DictionaryFilePath"];

            var dictionaryData = _wordLoader
                .LoadFromFile(dictionaryFilePath)
                .DistinctBy(dd => dd.Text.ToLower().Trim())
                .OrderBy(dd => dd.Text)
                .ToArray();

            _wordsRepository.AddWords(dictionaryData);
        }
    }
}
