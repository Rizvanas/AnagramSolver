using Contracts;
using Contracts.Entities;
using Contracts.Repositories;
using System;
using System.Collections.Generic;
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

        public void AddUserLog(UserLogEntity userLog)
        {
            throw new NotImplementedException();
        }

        public UserLogEntity GetUserLog(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserLogEntity> GetUserLogs()
        {
            throw new NotImplementedException();
        }
    }
}
