using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ARTHVATECH_ADMIN.DbContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public string ConnectionString { get; }

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(ConnectionString);

    }
}
