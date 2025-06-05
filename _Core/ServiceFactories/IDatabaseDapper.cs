using Dapper;
using System.Data;
using static Dapper.SqlMapper;

namespace BookStoreAPI._Core.ServiceFactories
{
    public interface IDatabaseDapper : IDisposable
    {
        Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<List<T>> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<DataTable> ExecuteAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<GridReader> ExecuteGridReaderAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<T> ExecuteGridReaderFirstOrDefaultAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure) where T : class;

        Task<IEnumerable<T>> ExecuteGridReaderEnumerableAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure) where T : class;

        Task<DataSet> ExecuteReaderAsync(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
    }
}
