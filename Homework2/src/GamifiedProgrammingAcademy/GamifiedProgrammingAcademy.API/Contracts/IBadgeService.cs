using GamifiedProgrammingAcademy.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Contracts
{
    public interface IBadgeService
    {
        Task<IEnumerable<BadgeResponseDto>> GetAllBadgesAsync();
        Task<BadgeResponseDto> GetBadgeByIdAsync(int id);
        Task<BadgeResponseDto> CreateBadgeAsync(CreateBadgeDto createBadgeDto);
        Task<BadgeResponseDto> UpdateBadgeAsync(int id, UpdateBadgeDto updateBadgeDto);
        Task<bool> DeleteBadgeAsync(int id);
        Task<IEnumerable<BadgeResponseDto>> GetUserBadgesAsync(int userId);
        Task<bool> AwardBadgeToUserAsync(int userId, int badgeId);
    }
}