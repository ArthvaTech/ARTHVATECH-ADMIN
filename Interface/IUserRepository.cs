using ARTHVATECH_ADMIN.Models;

namespace ARTHVATECH_ADMIN.Interface
{
    public interface IUserRepository
    {
        Users GetUserById(string Email);
        Users CreateUser(Users users);
        public List<MenuMaster> GetMaster();
        public string GenerateRandomString(int length);
    }
}
