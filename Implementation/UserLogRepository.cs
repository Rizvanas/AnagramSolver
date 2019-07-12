using Core.DTO;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                .Append("VALUES((SELECT Id FROM Phrases WHERE LOWER(REPLACE(Phrase, ' ', '')) = LOWER(REPLACE(@searchPhrase, ' ', ''))), ")
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

        public List<UserLog> GetUserLogs()
        {
            var logInsertionQuery = "SELECT FROM ";
            using (var command = new SqlCommand(logInsertionQuery, _connection) { CommandType = CommandType.Text })
            {
                command.Connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var userLogs = new List<UserLog>();
                    while (reader.Read())
                    {
                        userLogs.Add(new UserLog { });
                    }

                    command.Connection.Close();
                    return userLogs;
                }
            }
        }
    }
}
