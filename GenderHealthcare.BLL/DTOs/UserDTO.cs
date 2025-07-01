using System;

namespace GenderHealthcare.BLL.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public bool? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}