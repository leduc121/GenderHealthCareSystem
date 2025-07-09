using GenderHealthcare.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto);
        Task<RoleDTO> UpdateRoleAsync(RoleDTO roleDto);
        Task<bool> DeleteRoleAsync(string id);
        Task<RoleDTO> GetRoleByIdAsync(string roleId);
        Task<IEnumerable<RoleDTO>> GetRolesByUserIdAsync(string userId);
    }
}