using GenderHealthcare.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> GetUserByUsernameAsync(string username);
        Task<UserDTO> CreateUserAsync(UserDTO userDto);
        Task<UserDTO> UpdateUserAsync(UserDTO userDto);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> ValidateLoginAsync(string username, string password);
    }
}