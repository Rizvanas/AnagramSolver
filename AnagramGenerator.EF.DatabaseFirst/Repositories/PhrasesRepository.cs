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

        public void AddPhrase(Phrase phrase)
        {
            var result = _wordsDBContext.Phrases.Add(new PhraseEntity
            {
                Id = phrase.Id,
                Phrase = phrase.Text,
            });
        }

        public void AddPhrases(params Phrase[] phrases)
        {
            _wordsDBContext.Phrases
                .AddRange(phrases.Select(a => new PhraseEntity
                {
                    Id = a.Id,
                    Phrase = a.Text
                }));
        }

        public void DeletePhrase(int id)
        {
            var phrase = _wordsDBContext.Phrases.FirstOrDefault(p => p.Id == id);
            var result = _wordsDBContext.Phrases.Remove(phrase);
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
