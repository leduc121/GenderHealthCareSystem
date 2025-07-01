using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => MapToDTO(r));
        }

        public async Task<RoleDTO> GetRoleByIdAsync(string id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            return role != null ? MapToDTO(role) : null;
        }

        public async Task<RoleDTO> GetRoleByNameAsync(string name)
        {
            var role = await _roleRepository.GetByNameAsync(name);
            return role != null ? MapToDTO(role) : null;
        }

        public async Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto)
        {
            var role = MapToEntity(roleDto);
            var createdRole = await _roleRepository.AddAsync(role);
            return MapToDTO(createdRole);
        }

        public async Task<RoleDTO> UpdateRoleAsync(RoleDTO roleDto)
        {
            var role = MapToEntity(roleDto);
            var updatedRole = await _roleRepository.UpdateAsync(role);
            return MapToDTO(updatedRole);
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            return await _roleRepository.DeleteAsync(id);
        }

        private RoleDTO MapToDTO(Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                Status = role.Status,
                UpdatedAt = role.UpdatedAt
            };
        }

        private Role MapToEntity(RoleDTO roleDto)
        {
            return new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                Description = roleDto.Description,
                Status = roleDto.Status,
                UpdatedAt = roleDto.UpdatedAt
            };
        }
    }
}