using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Persistence.Data.Repository.Contract;

namespace Persistence.Data.Repository
{
    public class ExtendedRepository<T> : GenericRepository<T>, IExtendedRepository<T> where T : class
    {
        private readonly string _tableName;

        public ExtendedRepository(IDbConnection connection, IDbTransaction transaction, IConfiguration configuration) :
            base(connection, transaction, configuration)
        {
            _tableName = typeof(T).Name;
        }

        public async Task SoftDeleteRowAsync(Guid id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"UPDATE FROM {_tableName} SET IsDeleted=true WHERE Id=@Id",
                    new {Id = id});
            }
        }
    }
}