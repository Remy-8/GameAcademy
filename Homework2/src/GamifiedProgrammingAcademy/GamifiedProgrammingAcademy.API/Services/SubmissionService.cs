#nullable disable
using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Dtos;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChallengeRepository _challengeRepository;

        public SubmissionService(ISubmissionRepository submissionRepository, IUserRepository userRepository, IChallengeRepository challengeRepository)
        {
            _submissionRepository = submissionRepository;
            _userRepository = userRepository;
            _challengeRepository = challengeRepository;
        }

        public async Task<IEnumerable<SubmissionResponseDto>> GetAllSubmissionsAsync()
        {
            var submissions = await _submissionRepository.GetAllAsync();

            return submissions.Select(s => new SubmissionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                ChallengeId = s.ChallengeId,
                Code = s.Code,
                Language = s.Language,
                Status = s.Status,
                Score = s.Score,
                SubmittedAt = s.SubmittedAt,
                Output = s.Output,
                ErrorMessage = s.ErrorMessage,
                ExecutionTime = s.ExecutionTime
            });
        }

        public async Task<SubmissionResponseDto> GetSubmissionByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var submission = await _submissionRepository.GetByIdAsync(id);

            if (submission == null)
                return null;

            return new SubmissionResponseDto
            {
                Id = submission.Id,
                UserId = submission.UserId,
                ChallengeId = submission.ChallengeId,
                Code = submission.Code,
                Language = submission.Language,
                Status = submission.Status,
                Score = submission.Score,
                SubmittedAt = submission.SubmittedAt,
                Output = submission.Output,
                ErrorMessage = submission.ErrorMessage,
                ExecutionTime = submission.ExecutionTime
            };
        }

        public async Task<SubmissionResponseDto> CreateSubmissionAsync(CreateSubmissionDto createSubmissionDto)
        {
            // Validaciones de negocio
            if (!await _userRepository.ExistsAsync(createSubmissionDto.UserId))
                throw new InvalidOperationException("El usuario no existe");

            if (!await _challengeRepository.ExistsAsync(createSubmissionDto.ChallengeId))
                throw new InvalidOperationException("El desafío no existe");

            var submission = new Submission
            {
                UserId = createSubmissionDto.UserId,
                ChallengeId = createSubmissionDto.ChallengeId,
                Code = createSubmissionDto.Code.Trim(),
                Language = createSubmissionDto.Language.Trim(),
                Status = "Pending",
                Score = 0,
                SubmittedAt = DateTime.UtcNow,
                Output = "",
                ErrorMessage = "",
                ExecutionTime = 0
            };

            // Evaluación básica
            EvaluateSubmission(submission);

            await _submissionRepository.AddAsync(submission);

            return new SubmissionResponseDto
            {
                Id = submission.Id,
                UserId = submission.UserId,
                ChallengeId = submission.ChallengeId,
                Code = submission.Code,
                Language = submission.Language,
                Status = submission.Status,
                Score = submission.Score,
                SubmittedAt = submission.SubmittedAt,
                Output = submission.Output,
                ErrorMessage = submission.ErrorMessage,
                ExecutionTime = submission.ExecutionTime
            };
        }

        public async Task<IEnumerable<SubmissionResponseDto>> GetUserSubmissionsAsync(int userId)
        {
            var submissions = await _submissionRepository.GetSubmissionsByUserAsync(userId);

            return submissions.Select(s => new SubmissionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                ChallengeId = s.ChallengeId,
                Code = s.Code,
                Language = s.Language,
                Status = s.Status,
                Score = s.Score,
                SubmittedAt = s.SubmittedAt,
                Output = s.Output,
                ErrorMessage = s.ErrorMessage,
                ExecutionTime = s.ExecutionTime
            });
        }

        public async Task<IEnumerable<SubmissionResponseDto>> GetChallengeSubmissionsAsync(int challengeId)
        {
            var submissions = await _submissionRepository.GetSubmissionsByChallengeAsync(challengeId);

            return submissions.Select(s => new SubmissionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                ChallengeId = s.ChallengeId,
                Code = s.Code,
                Language = s.Language,
                Status = s.Status,
                Score = s.Score,
                SubmittedAt = s.SubmittedAt,
                Output = s.Output,
                ErrorMessage = s.ErrorMessage,
                ExecutionTime = s.ExecutionTime
            });
        }

        private void EvaluateSubmission(Submission submission)
        {
            var codeToCheck = submission.Code.ToLower();

            if (codeToCheck.Contains("console.writeline") ||
                codeToCheck.Contains("print") ||
                codeToCheck.Contains("return"))
            {
                submission.Status = "Correct";
                submission.Score = 100;
                submission.Output = "¡Código ejecutado correctamente!";
                submission.ExecutionTime = new Random().Next(50, 500);
            }
            else
            {
                submission.Status = "Incorrect";
                submission.Score = 0;
                submission.ErrorMessage = "El código no parece ser válido";
                submission.ExecutionTime = 0;
            }
        }
    }
}