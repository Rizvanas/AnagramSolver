using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public PhrasesRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddPhrase(Phrase phrase)
        {
            if (phrase == null)
                throw new ArgumentNullException("argument phrase is null");

            _wordsDB_CFContext.Phrases.Add(new PhraseEntity
            {
                Id = phrase.Id,
                Phrase = phrase.Text
            });

            _wordsDB_CFContext.SaveChanges();
        }

        public void AddPhrases(params Phrase[] phrases)
        {
            if (phrases == null || phrases.Length == 0)
                throw new ArgumentNullException("Argument anagrams is null or empty");

            _wordsDB_CFContext.Phrases.AddRange(phrases.Select(p => new PhraseEntity
            {
                Id = p.Id,
                Phrase = p.Text
            }));

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeletePhrase(int id)
        {
            var phraseEntity = _wordsDB_CFContext.Phrases.FirstOrDefault(p => p.Id == id);

            if (phraseEntity == null)
                throw new ArgumentException($"phrase by id of {id} not found");

            _wordsDB_CFContext.Phrases.Remove(phraseEntity);

            _wordsDB_CFContext.SaveChanges();
        }

        public Phrase GetPhrase(int id)
        {
            var phraseEntity = _wordsDB_CFContext.Phrases.FirstOrDefault(a => a.Id == id);

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
            var phraseEntities = _wordsDB_CFContext.Phrases;
            return phraseEntities.Select(p => new Phrase
            {
                Id = p.Id,
                Text = p.Phrase
            }).ToList();
        }
    }
}
