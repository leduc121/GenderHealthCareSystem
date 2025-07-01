using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
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

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => MapToDTO(u));
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? MapToDTO(user) : null;
        }

        public async Task<UserDTO> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user != null ? MapToDTO(user) : null;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            var user = MapToEntity(userDto);
            var createdUser = await _userRepository.AddAsync(user);
            return MapToDTO(createdUser);
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto)
        {
            var user = MapToEntity(userDto);
            var updatedUser = await _userRepository.UpdateAsync(user);
            return MapToDTO(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            return await _userRepository.ValidateLoginAsync(username, password);
        }

        private UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth, // Không cần chuyển đổi vì cả hai đều là DateOnly?
                Gender = user.Gender,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePicture,
                Status = user.Status,
                UpdatedAt = user.UpdatedAt,
                Username = user.Username,
                Password = user.Password
            };
        }

        private User MapToEntity(UserDTO userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateOfBirth = userDto.DateOfBirth, // Không cần chuyển đổi
                Gender = userDto.Gender,
                Phone = userDto.Phone,
                ProfilePicture = userDto.ProfilePicture,
                Status = userDto.Status,
                UpdatedAt = userDto.UpdatedAt,
                Username = userDto.Username,
                Password = userDto.Password
            };
        }
    }
}