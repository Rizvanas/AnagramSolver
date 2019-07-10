using Core.Domain;
using Core.DTO;
using Implementation.Extensions;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class SqlWordRepository : ISqlWordRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public SqlWordRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public bool AddCachedWord(Word word, List<string> anagrams)
        {
            throw new NotImplementedException();
        }

        public Word GetCachedWord(string phrase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Word> GetWords(PaginationFilter filter)
        {
            filter.Page = filter.Page ?? 1;
            using (var command = new SqlCommand(filter == null ? "GetWords" : "GetPaginizedWords", _connection)
            { CommandType = CommandType.StoredProcedure }) 
            {
                if (filter != null)
                {
                    command.Parameters.AddWithValue("@page", filter.Page);
                    command.Parameters.AddWithValue("@pageSize", filter.PageSize);
                }

                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var words = new List<Word>();
                    while (reader.Read())
                        words.Add(new Word { Text = reader.GetString(1) });

                    return words;
                }
            }
        }

        public IEnumerable<Word> SearchWords(string phrase)
        {
            var minWordLen = Convert.ToInt32(_appConfig.GetConfiguration()
                .GetSection("ConstantValues")["MinWordLength"]);

            using (var command = new SqlCommand("GetSearchWords", _connection)
            { CommandType = CommandType.StoredProcedure }) 
            {
                command.Parameters.AddWithValue("@phrase", phrase);
                command.Parameters.AddWithValue("@minLen", minWordLen);

                command.Connection.Open();
                using (var reader = command.ExecuteReader()) 
                {
                    var searchWords = new List<Word>();
                    while (reader.Read())
                        searchWords.Add(new Word { Text = reader.GetString(1) });

                    return searchWords
                        .Where(w => phrase.GetSearchWord(w.Text) != phrase)
                        .OrderByDescending(w => w.Text.Length);
                }
            }
        }

        public bool PutWords(string words)
        {
            throw new NotImplementedException();
        }

        public bool AddWord(Word word)
        {
            throw new NotImplementedException();
        }
    }
}
