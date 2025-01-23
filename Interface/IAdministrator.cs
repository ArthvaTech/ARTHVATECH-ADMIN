using ARTHVATECH_ADMIN.Models;

namespace ARTHVATECH_ADMIN.Interface
{
    public interface IAdministrator
    {
        public List<Users> GetUsers();
        public Users GetUserById(string Id);
        public List<Roles> GetRoles();
        public Users UpdateUser(Users users);
    }
}
