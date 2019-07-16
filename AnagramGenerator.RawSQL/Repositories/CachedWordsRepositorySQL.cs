﻿using Contracts;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.RawSQL.Repositories
{
    public class CachedWordsRepositorySQL : ICachedWordsRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public CachedWordsRepositorySQL(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public void AddCachedWord(CachedWordEntity cachedWord)
        {
            throw new NotImplementedException();
        }

        public void AddCachedWord(PhraseEntity phrase, IEnumerable<AnagramEntity> anagrams)
        {
            var phraseInsertionQuery = "INSERT INTO Phrases(Phrase) VALUES(@phrase);";
            var cacheInsertionQuery = new StringBuilder()
                .Append("INSERT INTO Anagrams(Anagram) VALUES(@anagram);")
                .Append("INSERT INTO CachedWords(PhraseId, AnagramId)")
                .Append("VALUES((SELECT Id FROM Phrases WHERE Phrase = @phrase),")
                .Append("(SELECT Id From Anagrams WHERE LOWER(REPLACE(Anagram, ' ', '')) = LOWER(REPLACE(@anagram, ' ', ''))));")
                .ToString();


            using (var command = new SqlCommand(phraseInsertionQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@phrase", phrase.Phrase);

                command.Connection.Open();
                command.ExecuteNonQuery();

                command.CommandText = cacheInsertionQuery;
                foreach (var anagram in anagrams)
                {
                    command.Parameters.AddWithValue("@anagram", anagram.Anagram);
                    command.ExecuteNonQuery();
                    command.Parameters.RemoveAt(command.Parameters.Count - 1);
                }
                command.Connection.Close();
            }
        }

        public IEnumerable<CachedWordEntity> GetCachedWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CachedWordEntity> GetCachedWords(PhraseEntity phrase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CachedWordEntity> GetCachedWords(AnagramEntity anagram)
        {
            throw new NotImplementedException();
        }
    }
}
