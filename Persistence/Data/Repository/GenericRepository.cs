using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Interface.Persistence.Repository;
using Infrastructure.Service;
using Microsoft.Extensions.Configuration;

namespace Persistence.Data.Repository
{
    public class GenericRepository<T> : ConnectionHelper, IGenericRepository<T> where T : class
    {
        private readonly string _tableName;
        private IDbTransaction Transaction { get; set; }
        private IDbConnection Connection { get; set; }

        public GenericRepository(IDbConnection connection, IDbTransaction transaction,
            IConfiguration configuration) : base(
            configuration)
        {
            _tableName = typeof(T).Name;
            Transaction = transaction;
            Connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Connection.QueryAsync<T>($"SELECT * FROM \"{_tableName}\"", transaction: Transaction);
        }

        public async Task DeleteRowAsync(Guid id)
        {
            await Connection.ExecuteAsync($"DELETE FROM \"{_tableName}\" WHERE Id=@Id", new {Id = id},
                transaction: Transaction);
        }

        public async Task<T> GetAsync(Guid id)
        {
            var result =
                await Connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM \"{_tableName}\" WHERE Id=@Id",
                    new {Id = id}, transaction: Transaction);
            if (result == null)
                throw new KeyNotFoundException($"\"{_tableName}\" with id [{id}] could not be found.");

            return result;
        }

        public async Task<int> SaveRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();

            inserted += await Connection.ExecuteAsync(query, list);

            return inserted;
        }

        public async Task UpdateAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();

            await Connection.ExecuteAsync(updateQuery, transaction: Transaction);
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE \"{_tableName}\" SET ");
            var properties = GenerateList.GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }

        public async Task InsertAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();

            await Connection.ExecuteAsync(insertQuery, transaction: Transaction);
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO \"{_tableName}\" ");

            insertQuery.Append("(");

            var properties = GenerateList.GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
    }
}