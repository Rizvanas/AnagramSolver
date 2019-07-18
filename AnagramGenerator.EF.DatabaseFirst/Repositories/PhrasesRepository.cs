using AnagramGenerator.EF.DatabaseFirst.Entities;
using Contracts;
using Contracts.DTO;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramGenerator.EF.DatabaseFirst.Repositories
{
    public class PhrasesRepository : IPhrasesRepository
    {
        private readonly WordsDBContext _wordsDBContext;

        public PhrasesRepository (WordsDBContext wordsDBContext)
        {
            _wordsDBContext = wordsDBContext;
        }

        public bool AddPhrase(Phrase phrase)
        {
            var result = _wordsDBContext.Phrases.Add(new PhraseEntity
            {
                Id = phrase.Id,
                Phrase = phrase.Text,
            });

            return result.State == EntityState.Added;
        }

        public bool AddPhrases(params Phrase[] phrases)
        {
            _wordsDBContext.Phrases
                .AddRange(phrases.Select(a => new PhraseEntity
                {
                    Id = a.Id,
                    Phrase = a.Text
                }));

            return true;
        }

        public bool DeletePhrase(int id)
        {
            var phrase = _wordsDBContext.Phrases.FirstOrDefault(p => p.Id == id);
            var result = _wordsDBContext.Phrases.Remove(phrase);

            return result.State == EntityState.Deleted;
        }

        public Phrase GetPhrase(int id)
        {
            var phraseEntity = _wordsDBContext.Phrases.FirstOrDefault(p => p.Id == id);

            if (phraseEntity == null)
                return null;

            return new Phrase
            {
                Id = phraseEntity.Id,
                Text = phraseEntity.Phrase
            };
        }

        public IList<Phrase> GetPhrases()
        {
            var phrases = _wordsDBContext.Phrases;

            return phrases.Select(p => new Phrase
            {
                Id = p.Id,
                Text = p.Phrase
            }).ToList();
        }
    }
}
