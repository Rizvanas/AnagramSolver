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
    public class UserLogsRepositorySQL : IUserLogsRepository
    {
        private readonly SqlConnection _connection;
        private readonly IAppConfig _appConfig;

        public UserLogsRepositorySQL(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public void AddUserLog(UserLog userLog)
        {
            var logInsertionQuery = new StringBuilder()
                .Append("INSERT INTO UserLog(SearchPhraseId, SearchTime, UserIp) ")
                .Append("VALUES((SELECT Id FROM Phrases WHERE LOWER(REPLACE(Phrase, ' ', '')) = ")
                .Append("LOWER(REPLACE(@searchPhrase, ' ', ''))), ")
                .Append("@searchTime, @userIP);")
                .ToString();

            using (var command = new SqlCommand(logInsertionQuery, _connection) { CommandType = CommandType.Text })
            {
                command.Parameters.AddWithValue("@searchPhrase", userLog.SearchPhrase);
                command.Parameters.AddWithValue("@searchTime", userLog.SearchTime);
                command.Parameters.AddWithValue("@userIP", userLog.UserIp);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public void AddUserLogs(params UserLog[] userLogs)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserLog(int id)
        {
            throw new NotImplementedException();
        }

        public UserLog GetUserLog(int id)
        {
            throw new NotImplementedException();
        }

        public IList<UserLog> GetUserLogs()
        {
            var logInsertionQuery = new StringBuilder()
                .Append("SELECT UserLog.UserIp, SearchTime, Phrase, Id ")
                .Append("FROM UserLog ")
                .Append("JOIN Phrases ON SearchPhraseId = Phrases.Id ")
                .Append("JOIN CachedWords ON PhraseId = Phrases.Id ")
                .Append("JOIN Anagrams ON Anagrams.Id = AnagramId;")
                .ToString();

            using (var command = new SqlCommand(logInsertionQuery, _connection) { CommandType = CommandType.Text })
            {
                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var userLogs = new List<UserLog>();
                    while (reader.Read())
                    {
                        userLogs.Add(new UserLog
                        {
                            UserIp = reader.GetString(0),
                            SearchTime = reader.GetInt32(1),
                        });
                    }
                    command.Connection.Close();

                    return userLogs;
                }
            }
        }
    }
}
