using Contracts;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.RawSQL.Repositories
{
    public class AnagramsRepositorySQL : IAnagramsRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public AnagramsRepositorySQL(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public void AddAnagram(AnagramEntity anagram)
        {

        }

        public void AddAnagrams(params AnagramEntity[] anagrams)
        {
            throw new NotImplementedException();
        }

        public AnagramEntity GetAnagram(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnagramEntity> GetAnagrams()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnagramEntity> GetAnagrams(PhraseEntity phrase)
        {
            var anagramsSelectionQuery = new StringBuilder()
                .Append("SELECT Anagram ")
                .Append("FROM CachedWords ")
                .Append("JOIN Phrases ON PhraseId = Phrases.Id ")
                .Append("JOIN Anagrams ON AnagramId = Anagrams.Id ")
                .Append("WHERE LOWER(REPLACE(Phrase, ' ', '')) = LOWER(REPLACE(@phrase, ' ', ''));")
                .ToString();

            using (var command = new SqlCommand(anagramsSelectionQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@phrase", phrase.Phrase);

                command.Connection.Open();
                command.ExecuteNonQuery();

                var anagrams = new List<AnagramEntity>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        anagrams.Add(new AnagramEntity { Anagram = reader.GetString(0) });

                    command.Connection.Close();
                    return anagrams;
                }
            }
        }
    }
}
