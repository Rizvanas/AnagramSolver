﻿using AnagramGenerator.EF.CodeFirst.Entities;
using Contracts.DTO;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGenerator.EF.CodeFirst.Repositories
{
    public class CachedWordsRepository : ICachedWordsRepository
    {
        private readonly WordsDB_CFContext _wordsDB_CFContext;

        public CachedWordsRepository(WordsDB_CFContext wordsDB_CFContext)
        {
            _wordsDB_CFContext = wordsDB_CFContext;
        }

        public void AddCachedWord(int phraseId, IEnumerable<Anagram> anagrams)
        {
            foreach (var anagram in anagrams)
            {
                var result = _wordsDB_CFContext.CachedWords.Add(new CachedWordEntity
                {
                    AnagramId = anagram.Id,
                    PhraseId = phraseId,
                    Anagram = new AnagramEntity
                    {
                        Id = anagram.Id,
                        Anagram = anagram.Text
                    },
                });

                if (result.State != EntityState.Added)
                    throw new InvalidOperationException("Failed to add CachedWords");
            }

            _wordsDB_CFContext.SaveChanges();
        }

        public void DeleteCachedWord(int id)
        {
            var cachedWordEntity = _wordsDB_CFContext.CachedWords.FirstOrDefault(cw => cw.Id == id);

            if (cachedWordEntity == null)
                throw new InvalidOperationException($"CachedWordEntity with id:{id} was not found");

            var result = _wordsDB_CFContext.CachedWords.Remove(cachedWordEntity);
            _wordsDB_CFContext.SaveChanges();
        }

        public CachedWord GetCachedWord(int id)
        {
            var cachedWordEntity = _wordsDB_CFContext.CachedWords.FirstOrDefault(cw => cw.Id == id);

            if (cachedWordEntity == null)
                throw new InvalidOperationException($"CachedWordEntity with id:{id} was not found");

            return new CachedWord
            {
                Id = cachedWordEntity.Id,
                AnagramId = cachedWordEntity.AnagramId,
                PhraseId = cachedWordEntity.PhraseId,
                Anagram = new Anagram
                {
                    Id = cachedWordEntity.Anagram.Id,
                    Text = cachedWordEntity.Anagram.Anagram
                },
                Phrase = new Phrase
                {
                    Id = cachedWordEntity.Phrase.Id,
                    Text = cachedWordEntity.Phrase.Phrase
                }
            };
        }

        public IList<CachedWord> GetCachedWords()
        {
            var cachedWordEntities = _wordsDB_CFContext.CachedWords;

            return cachedWordEntities.Select(cw => new CachedWord
            {
                Id = cw.Id,
                AnagramId = cw.AnagramId,
                PhraseId = cw.PhraseId,
                Anagram = new Anagram
                {
                    Id = cw.Anagram.Id,
                    Text = cw.Anagram.Anagram
                },
                Phrase = new Phrase
                {
                    Id = cw.Phrase.Id,
                    Text = cw.Phrase.Phrase
                }
            }).ToList();
        }
    }
}
