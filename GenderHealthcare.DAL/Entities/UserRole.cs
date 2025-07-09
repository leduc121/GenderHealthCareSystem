namespace GenderHealthcare.DAL.Entities
{
    public class UserRole
    {
        // Composite key gồm UserId + RoleId
        public string UserId { get; set; }
        public string RoleId { get; set; }

        // Quan hệ đến User và Role
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
