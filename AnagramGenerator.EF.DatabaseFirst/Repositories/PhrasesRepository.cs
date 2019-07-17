using Contracts;
using Contracts.DTO;
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

        public IEnumerable<Phrase> GetPhrases()
        {
            return _wordsDBContext.Phrases;
        }

        public Phrase GetPhrase(int id)
        {
            return _wordsDBContext.Phrases
                .FirstOrDefault(phrase => phrase.Id == id);
        }

        public Phrase GetPhrase(string phrase)
        {
            if (phrase == null)
                return null;

            var phrases = _wordsDBContext.Phrases
                .FirstOrDefault(ph => ph.Phrase.ToLower()
                == phrase.ToLower());

            return phrases;
        }

        public void AddPhrase(Phrase phrase)
        {
            _wordsDBContext.Phrases.Add(phrase);
            _wordsDBContext.SaveChanges();
        }
    }
}
