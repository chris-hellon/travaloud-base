using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Travaloud.Infrastructure.Contexts
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }

    public interface IDapperContext
    {
        public IDbConnection CreateConnection();
    }
}

