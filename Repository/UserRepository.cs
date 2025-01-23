using ARTHVATECH_ADMIN.DbContext;
using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;
using Dapper;
using System.Data;
using System.Text;

namespace ARTHVATECH_ADMIN.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext Db;
        public UserRepository(DapperContext context)
        {
            Db = context;
        }
       
        public Users GetUserById(string Email)
        {
            using (var connection = Db.CreateConnection()) // Assuming CreateConnection() opens a new SQL connection
            {
                var parameters = new DynamicParameters();
                parameters.Add("@username", Email, DbType.String);

                // Call the stored procedure
                var user = connection.QueryFirstOrDefault<Users>(
                    "CheckExist",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return user;
            }
        }
        public Users CreateUser(Users users)
        {
            using (var connection = Db.CreateConnection())
            {
                //connection.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@Id", users.Id, DbType.Guid);
                parameters.Add("@Username", users.Username, DbType.String);
                parameters.Add("@TempCode", users.TempCode, DbType.String);
                parameters.Add("@FirstName", users.FirstName, DbType.String);
                parameters.Add("@LastName", users.LastName, DbType.String);
                
                // Execute the stored procedure
                connection.Execute("InsertUser", parameters, commandType: CommandType.StoredProcedure);

                return users;
            }
        }
        public List<MenuMaster> GetMaster()
        {
            using (var connection = Db.CreateConnection())
            {
                //connection.Open();
                var menu = new List<MenuMaster>();
                var parameters = new DynamicParameters();

                // Execute the stored procedure
                menu = connection.Query<MenuMaster>("GetMenuMaster", parameters, commandType: CommandType.StoredProcedure).ToList();

                return menu;
            }
        }
        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }

    }
}
