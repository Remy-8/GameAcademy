using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Dtos;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using GamifiedProgrammingAcademy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;
        private readonly IUserRepository _userRepository;
        private readonly GamifiedAcademyContext _context; // Necesario para UserBadges

        public BadgeService(IBadgeRepository badgeRepository, IUserRepository userRepository, GamifiedAcademyContext context)
        {
            _badgeRepository = badgeRepository;
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<IEnumerable<BadgeResponseDto>> GetAllBadgesAsync()
        {
            var badges = await _badgeRepository.GetAllAsync();

            return badges.Select(b => new BadgeResponseDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                IconUrl = b.IconUrl,
                BadgeType = b.BadgeType,
                UnlockCondition = b.UnlockCondition,
                RequiredPoints = b.RequiredPoints,
                Rarity = b.Rarity
            });
        }

        public async Task<BadgeResponseDto> GetBadgeByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var badge = await _badgeRepository.GetByIdAsync(id);

            if (badge == null)
                return null;

            return new BadgeResponseDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                IconUrl = badge.IconUrl,
                BadgeType = badge.BadgeType,
                UnlockCondition = badge.UnlockCondition,
                RequiredPoints = badge.RequiredPoints,
                Rarity = badge.Rarity
            };
        }

        public async Task<BadgeResponseDto> CreateBadgeAsync(CreateBadgeDto createBadgeDto)
        {
            // Validación de negocio básica
            if (await BadgeNameExistsAsync(createBadgeDto.Name))
                throw new InvalidOperationException("Ya existe un badge con ese nombre");

            var badge = new Badge
            {
                Name = createBadgeDto.Name.Trim(),
                Description = createBadgeDto.Description.Trim(),
                IconUrl = createBadgeDto.IconUrl?.Trim(),
                BadgeType = createBadgeDto.BadgeType.Trim(),
                UnlockCondition = createBadgeDto.UnlockCondition.Trim(),
                RequiredPoints = createBadgeDto.RequiredPoints,
                Rarity = createBadgeDto.Rarity?.Trim() ?? "Common"
            };

            await _badgeRepository.AddAsync(badge);

            return new BadgeResponseDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                IconUrl = badge.IconUrl,
                BadgeType = badge.BadgeType,
                UnlockCondition = badge.UnlockCondition,
                RequiredPoints = badge.RequiredPoints,
                Rarity = badge.Rarity
            };
        }

        public async Task<BadgeResponseDto> UpdateBadgeAsync(int id, UpdateBadgeDto updateBadgeDto)
        {
            if (id <= 0)
                return null;

            var badge = await _badgeRepository.GetByIdAsync(id);

            if (badge == null)
                return null;

            // Validar que el nombre no esté duplicado (excepto el actual)
            if (await BadgeNameExistsAsync(updateBadgeDto.Name, id))
                throw new InvalidOperationException("Ya existe un badge con ese nombre");

            // Actualizar propiedades
            badge.Name = updateBadgeDto.Name.Trim();
            badge.Description = updateBadgeDto.Description.Trim();
            badge.IconUrl = updateBadgeDto.IconUrl?.Trim();
            badge.BadgeType = updateBadgeDto.BadgeType.Trim();
            badge.UnlockCondition = updateBadgeDto.UnlockCondition.Trim();
            badge.RequiredPoints = updateBadgeDto.RequiredPoints;
            badge.Rarity = updateBadgeDto.Rarity?.Trim() ?? "Common";

            await _badgeRepository.UpdateAsync(badge);

            return new BadgeResponseDto
            {
                Id = badge.Id,
                Name = badge.Name,
                Description = badge.Description,
                IconUrl = badge.IconUrl,
                BadgeType = badge.BadgeType,
                UnlockCondition = badge.UnlockCondition,
                RequiredPoints = badge.RequiredPoints,
                Rarity = badge.Rarity
            };
        }

        public async Task<bool> DeleteBadgeAsync(int id)
        {
            if (id <= 0)
                return false;

            var badge = await _badgeRepository.GetByIdAsync(id);

            if (badge == null)
                return false;

            await _badgeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<BadgeResponseDto>> GetUserBadgesAsync(int userId)
        {
            var badges = await _badgeRepository.GetBadgesByUserAsync(userId);

            return badges.Select(b => new BadgeResponseDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description,
                IconUrl = b.IconUrl,
                BadgeType = b.BadgeType,
                UnlockCondition = b.UnlockCondition,
                RequiredPoints = b.RequiredPoints,
                Rarity = b.Rarity
            });
        }

        public async Task<bool> AwardBadgeToUserAsync(int userId, int badgeId)
        {
            // Validar que existan usuario y badge
            if (!await _userRepository.ExistsAsync(userId) || !await _badgeRepository.ExistsAsync(badgeId))
                return false;

            // Verificar que el usuario no tenga ya este badge
            var existingUserBadge = await _context.UserBadges
                .AnyAsync(ub => ub.UserId == userId && ub.BadgeId == badgeId && ub.IsActive);

            if (existingUserBadge)
                return false; // El usuario ya tiene este badge

            // Crear la relación UserBadge
            var userBadge = new UserBadge
            {
                UserId = userId,
                BadgeId = badgeId,
                EarnedAt = DateTime.UtcNow,
                IsDisplayed = true,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.UserBadges.Add(userBadge);
            await _context.SaveChangesAsync();

            return true;
        }

        // Método privado para validación
        private async Task<bool> BadgeNameExistsAsync(string name, int? excludeId = null)
        {
            var badges = await _badgeRepository.GetAllAsync();
            var existingBadge = badges.FirstOrDefault(b => b.Name.ToLower() == name.ToLower());

            if (existingBadge == null) return false;
            if (excludeId.HasValue && existingBadge.Id == excludeId.Value) return false;

            return true;
        }
    }
}
