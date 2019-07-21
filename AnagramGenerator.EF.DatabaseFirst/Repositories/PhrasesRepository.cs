using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public PhrasesRepository(WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public void AddPhrase(Phrase phrase)
        {
            if (phrase == null)
                throw new ArgumentNullException("argument phrase is null");

            _wordsDBContext.Phrases.Add(new PhraseEntity
            {
                Id = phrase.Id,
                Phrase = phrase.Text
            });

            _wordsDBContext.SaveChanges();
        }

        public void AddPhrases(params Phrase[] phrases)
        {
            if (phrases == null || phrases.Length == 0)
                throw new ArgumentNullException("Argument anagrams is null or empty");

            _wordsDBContext.Phrases.AddRange(phrases.Select(p => new PhraseEntity
            {
                Id = p.Id,
                Phrase = p.Text
            }));

            _wordsDBContext.SaveChanges();
        }

        public void DeletePhrase(int id)
        {
            var phraseEntity = _wordsDBContext.Phrases.FirstOrDefault(p => p.Id == id);

            if (phraseEntity == null)
                throw new ArgumentException($"phrase by id of {id} not found");

            _wordsDBContext.Phrases.Remove(phraseEntity);

            _wordsDBContext.SaveChanges();
        }

        public Phrase GetPhrase(int id)
        {
            var phraseEntity = _wordsDBContext.Phrases.FirstOrDefault(a => a.Id == id);

            if (phraseEntity == null)
                throw new ArgumentException($"phrase by id of {id} not found");

            return new Phrase
            {
                Id = phraseEntity.Id,
                Text = phraseEntity.Phrase
            };
        }

        public IList<Phrase> GetPhrases()
        {
            var phraseEntities = _wordsDBContext.Phrases;
            return phraseEntities.Select(p => new Phrase
            {
                Id = p.Id,
                Text = p.Phrase
            }).ToList();
        }
    }
}
