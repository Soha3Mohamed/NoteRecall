using NoteRecall_Application.DTOs.UserDTOs;
using NoteRecall_Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.ServiceInterfaces
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<UserResponseDTO>>> GetAllUsersAsync();

        Task<ServiceResult<UserResponseDTO>> GetUserByEmailAsync(string email);
        Task<ServiceResult<UserResponseDTO>> GetUserByIdAsync(int id);
        Task<ServiceResult<UserResponseDTO>> RegisterUserAsync(UserRequestDTO userRequest);
        Task<ServiceResult<UserResponseDTO>> UpdateUserAsync(int id, UserUpdateDTO userRequest);
        Task<ServiceResult<bool>> DeleteUserAsync(int id);

        Task<ServiceResult<string>> AuthenticateUserAsync(string email, string password);

        Task<ServiceResult<string>> ChangePasswordAsync(int userId, string currentPassword, string newPassword);
       
    }
}
