using Contracts;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public PhraseEntity GetPhrase(int id)
        {
            throw new NotImplementedException();
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
