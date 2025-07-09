using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using System;
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
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Status = r.Status ?? false
            });
        }

        public async Task<RoleDTO> CreateRoleAsync(RoleDTO roleDto)
        {
            var role = new Role
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleDto.Name,
                Description = roleDto.Description,
                Status = roleDto.Status
            };

            var createdRole = await _roleRepository.AddAsync(role);
            return new RoleDTO
            {
                Id = createdRole.Id,
                Name = createdRole.Name,
                Description = createdRole.Description,
                Status = createdRole.Status ?? false
            };
        }

        public async Task<RoleDTO> UpdateRoleAsync(RoleDTO roleDto)
        {
            var role = new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                Description = roleDto.Description,
                Status = roleDto.Status
            };

            var updatedRole = await _roleRepository.UpdateAsync(role);
            return new RoleDTO
            {
                Id = updatedRole.Id,
                Name = updatedRole.Name,
                Description = updatedRole.Description,
                Status = updatedRole.Status ?? false
            };
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            return await _roleRepository.DeleteAsync(id);
        }

        public async Task<RoleDTO> GetRoleByIdAsync(string roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                return null;
            }
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                Status = role.Status ?? false
            };
        }

        public async Task<IEnumerable<RoleDTO>> GetRolesByUserIdAsync(string userId)
        {
            var roles = await _roleRepository.GetRolesByUserIdAsync(userId);
            return roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                Status = r.Status ?? false
            });
        }
    }
}