using ARTHVATECH_ADMIN.DbContext;
using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;
using Dapper;
using System.Data;

namespace ARTHVATECH_ADMIN.Repository
{
    public class AdministratorRepository : IAdministrator
    {
        private readonly DapperContext Db;
        public AdministratorRepository(DapperContext context)
        {
            Db = context;
        }
        public List<Users> GetUsers()
        {
            using (var connection = Db.CreateConnection()) // Assuming CreateConnection() opens a new SQL connection
            {
                var user = new List<Users>();
                var parameters = new DynamicParameters();
                ////parameters.Add("@username", Email, DbType.String);

                // Call the stored procedure
                user = connection.Query<Users>(
                    "GetAllUsers",
                    parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return user;
            }
        }
    }
}
