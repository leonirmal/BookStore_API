using BookStoreAPI._Core.Extensions;
using BookStoreAPI.Domain.Global;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BookStoreAPI._Core.ServiceFactories
{
    public class DatabaseDapperExtension : IDatabaseDapper, IDisposable
    {
        readonly ILogger<DatabaseDapperExtension> _ilogger;

        string ConnectionString { get; }
        SqlConnection CurrentConnection { get; set; }


        public DatabaseDapperExtension(ILogger<DatabaseDapperExtension> ilogger, ConfigurationMaster configuration)
        {
            _ilogger = ilogger;
            ConnectionString = configuration.ConnectionStrings.Connection;
        }

        public void Dispose()
        {
            if (CurrentConnection.State != ConnectionState.Closed || CurrentConnection.State != ConnectionState.Broken)
                CurrentConnection.Close();
            GC.Collect();
        }

        public async Task<DataTable> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            SqlConnection db = GetDbconnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    var result = await db.ExecuteScalarAsync<DataTable>(sp, parms, null, null, commandType);
                    return result;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex?.GetDetailException());
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex?.GetDetailException());
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return null;
        }

        public async Task<T> ExecuteGridReaderFirstOrDefaultAsync<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure) where T : class
        {
            SqlConnection db = GetDbconnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    var result = await db.QueryMultipleAsync(sp, parms, null, 600, commandType);
                    return await result?.ReadFirstOrDefaultAsync<T>();
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex?.GetDetailException());
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex?.GetDetailException());
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return null;
        }

        public async Task<IEnumerable<T>> ExecuteGridReaderEnumerableAsync<T>(string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure) where T : class
        {
            SqlConnection db = GetDbconnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {

                    var result = await db.QueryMultipleAsync(sp, parms, null, 600, commandType);
                    return await result?.ReadAsync<T>();
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex?.GetDetailException());
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex?.GetDetailException());
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return null;
        }

        public async Task<SqlMapper.GridReader> ExecuteGridReaderAsync(
            string sp, DynamicParameters parms,
            CommandType commandType = CommandType.StoredProcedure)
        {
            SqlConnection db = GetDbconnection();
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                try
                {
                    CurrentConnection = db;
                    var result = await db.QueryMultipleAsync(sp, parms, null, 600, commandType);
                    return result;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex?.GetDetailException());
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex?.GetDetailException());
            }
            return null;
        }

        public async Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using SqlConnection db = GetDbconnection();
            var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return result.FirstOrDefault();
        }

        public async Task<List<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using SqlConnection db = GetDbconnection();
            var result = await db.QueryAsync<T>(sp, parms, commandType: commandType);
            return result.ToList();
        }

        public async Task<DataSet> ExecuteReaderAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            SqlConnection db = GetDbconnection();
            DataSet ds = new DataSet();
            var parameters = new DynamicParameters(parms);
            try
            {



                if (db.State == ConnectionState.Closed)
                    db.Open();



                try
                {
                    var _result = await db.ExecuteReaderAsync(sp, parameters, null, 600, CommandType.StoredProcedure);
                    int i = 1;
                    while (!_result.IsClosed)
                    {
                        ds.Tables.Add("Table" + i);
                        ds.EnforceConstraints = false;
                        ds.Tables[i - 1].Load(_result);
                        i++;
                    }



                    return ds;
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex?.GetDetailException());
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex?.GetDetailException());
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return null;
        }

        private SqlConnection GetDbconnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
