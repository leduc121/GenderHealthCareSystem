namespace GenderHealthcare.BLL.DTOs
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}