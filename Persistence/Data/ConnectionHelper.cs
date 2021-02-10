using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Data
{
    public abstract class ConnectionHelper
    {
        private readonly IConfiguration _configuration;

        protected ConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate new connection based on connection string
        /// </summary>
        /// <returns></returns>
        protected NpgsqlConnection GetSqlConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Open new connection and return it for use
        /// </summary>
        /// <returns></returns>
        protected IDbConnection CreateConnection()
        {
            var conn = GetSqlConnection();
            conn.Open();
            return conn;
        }
    }
}