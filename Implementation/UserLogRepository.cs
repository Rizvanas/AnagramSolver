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
            var logInsertionQuery = "INSERT INTO UserLog(SearchPhrase, SearchTime, UserIp) VALUES(@searchPhrase, @searchTime, @userIP);";
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
    }
}
