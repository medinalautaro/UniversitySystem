using Npgsql;
using SystemUniversity.Contracts.Models;
using SystemUniversity.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemUniversity.Persistence.Repositories
{
    internal abstract class RepositoryDB<T> : IRepository<T>
    {
        private NpgsqlDataSource _dataSource;

        public RepositoryDB(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public abstract Task CreateAsync(T entity);
        public abstract Task DeleteAsync(int entityId);
        public abstract Task UpdateAsync(T entity);
        public abstract Task<IEnumerable<T>> SelectAllAsync();

        protected async Task<int> ExecuteNonQueryAsync(string query, object[]? parameters = null)
        {
            using (NpgsqlCommand command = _dataSource.CreateCommand(query)) 
            {
                if(parameters != null) {
                    command.Parameters.AddRange(parameters.Select(x => new NpgsqlParameter(null, x)).ToArray());
                }

                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("No se pudo ejecutar la query");
                }

                return rowsAffected;
            }
        }

        protected async Task<RetType> ExecuteScalarAsync<RetType>(string query, object[]? parameters = null)
        {
            using (NpgsqlCommand command = _dataSource.CreateCommand(query))
            {
                if(parameters != null) {
                    command.Parameters.AddRange(parameters.Select(x => new NpgsqlParameter(null, x)).ToArray());
                }
                object? scalar = await command.ExecuteScalarAsync();

                if (scalar == null)
                {
                    throw new Exception("Query could not execute");
                }
                
                return (RetType)scalar;
            }
        }

        protected async Task<int> ExecuteScalarIntAsync(string query, object[]? parameters = null)
        {
            return await ExecuteScalarAsync<int>(query, parameters);
        }

        protected async Task<NpgsqlDataReader> GetQueryReaderAsync(string query, object[]? parameters = null)
        {
            using (NpgsqlCommand command = _dataSource.CreateCommand(query))
            {
                if(parameters != null) {
                    command.Parameters.AddRange(parameters.Select(x => new NpgsqlParameter(null, x)).ToArray());
                }

                return await command.ExecuteReaderAsync();
            }
        }
    }
}
