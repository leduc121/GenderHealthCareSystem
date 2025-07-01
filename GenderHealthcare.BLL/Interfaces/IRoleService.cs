using GenderHealthcare.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO> GetRoleByIdAsync(string id);
        Task<RoleDTO> GetRoleByNameAsync(string name);
        Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto);
        Task<RoleDTO> UpdateRoleAsync(RoleDTO roleDto);
        Task<bool> DeleteRoleAsync(string id);
    }
}