﻿namespace ARTHVATECH_ADMIN.Models
{
    public class Users
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? FirstName {  get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public Guid? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? TempCode { get; set; }
        public bool IsActive {  get; set; }
        public string? Designation {  get; set; }
    }
}
