using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NoteRecall_Application.Common;
using NoteRecall_Application.DTOs.UserDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteRecall_Application.ServiceImplementation
{
    internal class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly string _jwtKey = "secretKeyforjwtauthenticationforPayWise"; // should come from config

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<UserResponseDTO>>> GetAllUsersAsync()
        {
           var users = await _userRepository.GetAllAsync();
           if (users == null || !users.Any())
           {
                _logger.LogWarning("No users found in the database.");
                return ServiceResult<IEnumerable<UserResponseDTO>>.Fail("No users found.");
           }
           _logger.LogInformation("Retrieved {count} users from the database."  , users.Count());
            var dtoList = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return ServiceResult<IEnumerable<UserResponseDTO>>.Ok(dtoList);
        }

        public async Task<ServiceResult<UserResponseDTO>> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);/////
            if (user == null)
            {
                _logger.LogWarning("No user was found with this email : {email}", email);
                return ServiceResult<UserResponseDTO>.Fail("No user was found with this email");
            }
            _logger.LogInformation("Retrieved user with email: {email}", email);
            var userDto = _mapper.Map<UserResponseDTO>(user);
            return ServiceResult<UserResponseDTO>.Ok(userDto);
        }

        public async Task<ServiceResult<UserResponseDTO>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);/////
            if (user == null)
            {
                _logger.LogWarning("No user was found with this Id : {id}", id);
                return ServiceResult<UserResponseDTO>.Fail("No user was found with this Id");
            }
            _logger.LogInformation("Retrieved user with Id: {id}", id);
            var userDto = _mapper.Map<UserResponseDTO>(user);
            return ServiceResult<UserResponseDTO>.Ok(userDto);
        }

        public async Task<ServiceResult<UserResponseDTO>> RegisterUserAsync(UserRequestDTO userRequest)
        {
            var existingUser = await _userRepository.GetByEmailAsync(userRequest.Email);
            if(existingUser != null)
            {
                _logger.LogWarning("Registeration Failed. Email: {email} already exists", userRequest.Email);
                return ServiceResult<UserResponseDTO>.Fail($"Email: {userRequest.Email} already exists");
            }
            var user = _mapper.Map<User>(userRequest);
            user.PasswordHash = PasswordHasher.Hash(userRequest.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.LastUpdatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User {Email} registered successfully" , user.Email);


            var userDto = _mapper.Map<UserResponseDTO>(user);
            return ServiceResult<UserResponseDTO>.Ok(userDto);
        }
        public async Task<ServiceResult<UserResponseDTO>> UpdateUserAsync(int id, UserUpdateDTO userUpdate)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if(user == null)
            {
                _logger.LogWarning("User with this Id: {id} doesn't exist", id);
                return ServiceResult<UserResponseDTO>.Fail($"User with Id: {id} doesn't exist");
            }
            var existingEmailUser = await _userRepository.GetByEmailAsync(userUpdate.Email);
            if(existingEmailUser != null)
            {
                _logger.LogWarning("User with this Email: {Email} already exists", userUpdate.Email);
                return ServiceResult<UserResponseDTO>.Fail($"User with Email: {userUpdate.Email} already exists");
            }
            _mapper.Map(userUpdate, user);
            user.LastUpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            _logger.LogInformation("User {Email} updated successfully", user.Email);

            var dto = _mapper.Map<UserResponseDTO>(user);
            return ServiceResult<UserResponseDTO>.Ok(dto);
        }

        public async Task<ServiceResult<bool>> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User with id {Id} not found", id);
                return ServiceResult<bool>.Fail($"User with this id: {id} is Not Found");
            }
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
            _logger.LogInformation("User with id {Id} deleted successfully", id);
            return ServiceResult<bool>.Ok(true);
        }

        public async Task<ServiceResult<string>> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                _logger.LogWarning("Authentication failed for email {Email}", email);
                return ServiceResult<string>.Fail("Authentication Failed, Credentials don't match");
            }

            _logger.LogInformation("Authentication succeeded for email {Email}", email);

            var token = GenerateToken(user);

            return ServiceResult<string>.Ok(token);
        }

        public async Task<ServiceResult<string>> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with id {Id} not found", userId);
                return ServiceResult<string>.Fail("User not found");
            }
            if (PasswordHasher.VerifyPassword(user.PasswordHash, newPassword))
            {
                return ServiceResult<string>.Fail("The new password is the same as your old one");
            }
            user.PasswordHash = PasswordHasher.Hash(newPassword);
            await _userRepository.SaveChangesAsync();
            _logger.LogInformation("User {password} updated successfully", newPassword);
            return ServiceResult<string>.Ok($"The new password is: {newPassword}");
        }
     

        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtKey));
            var signingKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingKey
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
