using Contracts;
using Contracts.Entities;
using Contracts.Extensions;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AnagramGenerator.RawSQL.Repositories
{
    public class PhrasesRepositorySQL : IPhrasesRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public PhrasesRepositorySQL(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public void AddPhrase(PhraseEntity phrase)
        {
            var phrasesQuery = "INSERT INTO Phrases(Phrase) VALUES(@phrase);";

            using (var command = new SqlCommand(phrasesQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@phrase", phrase.Phrase);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public PhraseEntity GetPhrase(int id)
        {
            var phrasesQuery = "SELECT * FROM Phrases WHERE Phrases.Id == @id";
            using (var command = new SqlCommand(phrasesQuery, _connection)
            { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@id", id);
                var phrase = new PhraseEntity();

                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        phrase = new PhraseEntity
                        {
                            Id = Convert.ToInt32(reader.GetString(0)),
                            Phrase = reader.GetString(1)
                        };
                }
                command.Connection.Close();

                return phrase;
            }
        }

        public PhraseEntity GetPhrase(string phrase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PhraseEntity> GetPhrases()
        {
            throw new NotImplementedException();
        }
    }
}
