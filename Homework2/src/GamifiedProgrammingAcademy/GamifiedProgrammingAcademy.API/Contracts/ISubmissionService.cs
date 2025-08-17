using GamifiedProgrammingAcademy.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Contracts
{
    public interface ISubmissionService
    {
        Task<IEnumerable<SubmissionResponseDto>> GetAllSubmissionsAsync();
        Task<SubmissionResponseDto> GetSubmissionByIdAsync(int id);
        Task<SubmissionResponseDto> CreateSubmissionAsync(CreateSubmissionDto createSubmissionDto);
        Task<IEnumerable<SubmissionResponseDto>> GetUserSubmissionsAsync(int userId);
        Task<IEnumerable<SubmissionResponseDto>> GetChallengeSubmissionsAsync(int challengeId);
    }
}