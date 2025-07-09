using GenderHealthcare.BLL.DTOs;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Mã (ID) của user đã xác thực thành công
        /// </summary>
        string? UserId { get; set; }

        /// <summary>
        /// Lấy danh sách vai trò của người dùng hiện tại
        /// </summary>
        Task<IEnumerable<RoleDTO>> GetUserRolesAsync();
    }
}
