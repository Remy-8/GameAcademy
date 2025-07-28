using GamifiedProgrammingAcademy.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Contracts
{
    public interface IChallengeService
    {
        Task<IEnumerable<ChallengeResponseDto>> GetAllChallengesAsync();
        Task<ChallengeResponseDto> GetChallengeByIdAsync(int id);
        Task<ChallengeResponseDto> CreateChallengeAsync(CreateChallengeDto createChallengeDto);
        Task<ChallengeResponseDto> UpdateChallengeAsync(int id, UpdateChallengeDto updateChallengeDto);
        Task<bool> DeleteChallengeAsync(int id);
        Task<bool> ChallengeExistsAsync(int id);
    }
}
