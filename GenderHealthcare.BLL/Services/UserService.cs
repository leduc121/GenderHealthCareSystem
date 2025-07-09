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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user != null && user.Password == password; // Nên sử dụng hashing
        }

        public async Task<UserDTO> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePicture,
                Status = user.Status ?? false    // nullable -> non-nullable
            };
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = userDto.Username,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth,
                Gender = userDto.Gender,
                Phone = userDto.Phone,
                ProfilePicture = userDto.ProfilePicture,
                Status = userDto.Status,            // DTO.Status đã là bool, gán vào bool?
                Password = "default_password"       // Nên yêu cầu người dùng nhập mật khẩu và hash
            };

            var createdUser = await _userRepository.AddAsync(user);
            return new UserDTO
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                DateOfBirth = createdUser.DateOfBirth,
                Gender = createdUser.Gender,
                Phone = createdUser.Phone,
                ProfilePicture = createdUser.ProfilePicture,
                Status = createdUser.Status ?? false
            };
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto)
        {
            var user = new User
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth,
                Gender = userDto.Gender,
                Phone = userDto.Phone,
                ProfilePicture = userDto.ProfilePicture,
                Status = userDto.Status
            };

            var updatedUser = await _userRepository.UpdateAsync(user);
            return new UserDTO
            {
                Id = updatedUser.Id,
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                DateOfBirth = updatedUser.DateOfBirth,
                Gender = updatedUser.Gender,
                Phone = updatedUser.Phone,
                ProfilePicture = updatedUser.ProfilePicture,
                Status = updatedUser.Status ?? false
            };
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            bool? result = await _userRepository.DeleteAsync(id);
            return result ?? false;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                Gender = u.Gender,
                Phone = u.Phone,
                ProfilePicture = u.ProfilePicture,
                Status = u.Status ?? false
            });
        }
    }
}
