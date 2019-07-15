using Contracts;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public PhrasesRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public IEnumerable<PhraseEntity> GetPhrases()
        {
            return _wordsDBContext.Phrases;
        }

        public PhraseEntity GetPhrase(int id)
        {
            return _wordsDBContext.Phrases
                .FirstOrDefault(phrase => phrase.Id == id);
        }

        public PhraseEntity GetPhrase(string phrase)
        {
            return _wordsDBContext.Phrases
                .FirstOrDefault(ph => ph.Phrase.Replace(" ", "").ToLower() 
                == phrase.Replace(" ", "").ToLower());
        }

        public bool AddPhrase(PhraseEntity phrase)
        {
            if (_wordsDBContext.Phrases.Contains(phrase))
                return false;

            _wordsDBContext.Add(phrase);
            return true;
        }
    }
}
