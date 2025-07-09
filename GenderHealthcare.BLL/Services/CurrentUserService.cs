using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.UI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IRoleService _roleService;

        public CurrentUserService(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        public string? UserId { get; set; }

        public async Task<IEnumerable<RoleDTO>> GetUserRolesAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    return new List<RoleDTO>();
                }

                // Gọi IRoleService để lấy danh sách vai trò
                var roles = await _roleService.GetRolesByUserIdAsync(UserId);
                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy vai trò: {ex.Message}");
                return new List<RoleDTO>();
            }
        }
    }
}