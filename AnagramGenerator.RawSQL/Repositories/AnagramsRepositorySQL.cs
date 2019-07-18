using Contracts;
using Contracts.DTO;
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

        public void AddAnagram(Anagram anagram)
        {
            throw new NotImplementedException();
        }

        public void AddAnagrams(params Anagram[] anagrams)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnagram(int id)
        {
            throw new NotImplementedException();
        }

        public Anagram GetAnagram(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Anagram> GetAnagrams(Phrase phrase)
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
                command.Parameters.AddWithValue("@phrase", phrase.Text);

                command.Connection.Open();
                command.ExecuteNonQuery();

                var anagrams = new List<Anagram>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        anagrams.Add(new Anagram { Text = reader.GetString(0) });

                    command.Connection.Close();
                    return anagrams;
                }
            }
        }

        public IList<Anagram> GetAnagrams()
        {
            throw new NotImplementedException();
        }
    }
}
