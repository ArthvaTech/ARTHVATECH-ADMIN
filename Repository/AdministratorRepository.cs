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
        public Users GetUserById(string Id)
        {
            using (var connection = Db.CreateConnection()) // Assuming CreateConnection() opens a new SQL connection
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", Id, DbType.String);

                // Call the stored procedure
                var user = connection.QueryFirstOrDefault<Users>(
                    "GetUserDetails",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return user;
            }
        }
        public List<Roles> GetRoles()
        {
            using (var connection = Db.CreateConnection()) // Assuming CreateConnection() opens a new SQL connection
            {
                var parameters = new DynamicParameters();
                
                // Call the stored procedure
                var user = connection.Query<Roles>(
                    "GetRoles",
                    parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return user;
            }
        }
        public Users UpdateUser(Users users)
        {
            using (var connection = Db.CreateConnection()) 
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", users.Id, DbType.Guid);
                parameters.Add("@FirstName", users.FirstName ?? string.Empty, DbType.String);
                parameters.Add("@LastName", users.LastName ?? string.Empty, DbType.String);
                parameters.Add("@Designation", users.Designation ?? string.Empty, DbType.String);
                parameters.Add("@RoleId", users.RoleId ?? Guid.Empty, DbType.Guid);


                // Call the stored procedure
                var user = connection.Execute(
                    "UpdateUserDetails",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            return users;
        }
    }
}
