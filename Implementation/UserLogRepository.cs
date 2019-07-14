using Core.Domain;
using Core.DTO;
using Core.DTO.Responses;
using Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Implementation
{
    public class UserLogRepository : IUserLogRepository
    {
        private readonly IAppConfig _appConfig;
        private readonly SqlConnection _connection;

        public UserLogRepository(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _connection = new SqlConnection
            { ConnectionString = _appConfig.GetConnectionString() };
        }

        public bool AddUserLog(UserLog userLog)
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
                command.Parameters.AddWithValue("@userIP", userLog.UserIP);

                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }

            return true;
        }

        public List<UserLogResponse> GetUserLogs()
        {
            var logInsertionQuery = new StringBuilder()
                .Append("SELECT UserLog.UserIp, SearchTime, Phrase, Anagram ")
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
                    var userLogs = new List<UserLogResponse>();
                    while (reader.Read())
                    {
                        userLogs.Add(new UserLogResponse
                        {
                            UserIp = reader.GetString(0),
                            SearchTime = reader.GetInt32(1),
                            SearchPhrase = reader.GetString(2),
                            Anagram = new Word { Text = reader.GetString(3) }
                        });
                    }
                    command.Connection.Close();

                    return userLogs;
                }
            }
        }
    }
}
