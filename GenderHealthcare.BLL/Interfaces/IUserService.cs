using GenderHealthcare.BLL.DTOs;
using System.Threading.Tasks;

namespace GenderHealthcare.BLL.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateLoginAsync(string username, string password);
        Task<UserDTO> GetByUsernameAsync(string username);

        Task<UserDTO> CreateUserAsync(UserDTO userDto);
        Task<UserDTO> UpdateUserAsync(UserDTO userDto);
        Task<bool> DeleteUserAsync(string id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}