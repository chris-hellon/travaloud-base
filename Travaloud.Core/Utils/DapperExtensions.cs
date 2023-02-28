using System.Data;
using Dapper;

namespace Travaloud.Core.Utils
{
    public static class DapperExtensions
    {
        public static async Task<IEnumerable<T>> ExecuteGetStoredProcedureList<T>(this IDbConnection connection, string query, object request = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                if (request != null)
                {
                    return await connection.QueryAsync<T>(query, request,
                        commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return await connection.QueryAsync<T>(query,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<T> ExecuteGetStoredProcedureSingle<T>(this IDbConnection connection, string query, object request = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                if (request != null)
                {
                    return await connection.QuerySingleOrDefaultAsync<T>(query, request,
                        commandType: CommandType.StoredProcedure);
                }
                else
                {
                    return await connection.QuerySingleOrDefaultAsync<T>(query,
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<int> ExecuteInsertStoredProcedureSingle(this IDbConnection connection, string query, object request = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                return await connection.ExecuteScalarAsync<int>(query, request, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<T> ExecuteInsertStoredProcedureSingle<T>(this IDbConnection connection, string query, object request = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                return await connection.ExecuteScalarAsync<T>(query, request, commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<int> ExecuteUpdateStoredProcedureSingle(this IDbConnection connection, string query, object request = null)
        {
            return await ExecuteInsertStoredProcedureSingle(connection, query, request);
        }
        public static async Task<int> ExecuteDeleteStoredProcedureSingle(this IDbConnection connection, string query, object request = null)
        {
            return await ExecuteInsertStoredProcedureSingle(connection, query, request);
        }

        public static async Task<IEnumerable<T>> GetList<T>(this IDbConnection connection, string query, object request = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                var result = await connection.QueryAsync<T>(query);

                var enumerable = result as T[] ?? result.ToArray();
                return enumerable.Any() ? enumerable : Array.Empty<T>();
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<T> GetSingle<T>(this IDbConnection connection, string query)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                var result = await connection.QuerySingleOrDefaultAsync<T>(query);

                return result;
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<int> InsertSingle(this IDbConnection connection, string query, object parameters = null)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                }

                var returnedId = await connection.ExecuteScalarAsync<long>(query, parameters);

                return unchecked((int)returnedId);
            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static async Task<int> UpdateSingle(this IDbConnection connection, string query, object parameters = null)
        {
            return await InsertSingle(connection, query, parameters);
        }

        public static async Task<int> DeleteSingle(this IDbConnection connection, string query, object parameters = null)
        {
            return await InsertSingle(connection, query, parameters);
        }
    }
}

