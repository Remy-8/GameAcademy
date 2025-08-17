using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Dtos;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                TotalPoints = u.TotalPoints,
                Level = u.Level,
                ExperiencePoints = u.ExperiencePoints,
                JoinDate = u.JoinDate
            });
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalPoints = user.TotalPoints,
                Level = user.Level,
                ExperiencePoints = user.ExperiencePoints,
                JoinDate = user.JoinDate
            };
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Validaciones de negocio básicas
            if (await UsernameExistsAsync(createUserDto.Username))
                throw new InvalidOperationException("Ya existe un usuario con ese nombre de usuario");

            if (await EmailExistsAsync(createUserDto.Email))
                throw new InvalidOperationException("Ya existe un usuario con ese email");

            var user = new User
            {
                Username = createUserDto.Username.Trim(),
                Email = createUserDto.Email.Trim().ToLower(),
                FirstName = createUserDto.FirstName.Trim(),
                LastName = createUserDto.LastName.Trim(),
                TotalPoints = 0,
                Level = 1,
                ExperiencePoints = 0,
                JoinDate = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalPoints = user.TotalPoints,
                Level = user.Level,
                ExperiencePoints = user.ExperiencePoints,
                JoinDate = user.JoinDate
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            if (id <= 0)
                return null;

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            // Validar que el email no esté duplicado (excepto el actual)
            if (await EmailExistsAsync(updateUserDto.Email, id))
                throw new InvalidOperationException("Ya existe un usuario con ese email");

            // Actualizar propiedades
            user.Email = updateUserDto.Email.Trim().ToLower();
            user.FirstName = updateUserDto.FirstName.Trim();
            user.LastName = updateUserDto.LastName.Trim();

            await _userRepository.UpdateAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalPoints = user.TotalPoints,
                Level = user.Level,
                ExperiencePoints = user.ExperiencePoints,
                JoinDate = user.JoinDate
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
                return false;

            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _userRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<UserResponseDto>> GetTopUsersAsync(int count)
        {
            var users = await _userRepository.GetTopUsersByPointsAsync(count);

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                TotalPoints = u.TotalPoints,
                Level = u.Level,
                ExperiencePoints = u.ExperiencePoints,
                JoinDate = u.JoinDate
            });
        }

        // Métodos privados para validaciones
        private async Task<bool> UsernameExistsAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user != null;
        }

        private async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return false;
            if (excludeId.HasValue && user.Id == excludeId.Value) return false;
            return true;
        }
    }
}