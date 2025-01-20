namespace ARTHVATECH_ADMIN.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

    }
}
