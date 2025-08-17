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
    public class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<IEnumerable<ChallengeResponseDto>> GetAllChallengesAsync()
        {
            var challenges = await _challengeRepository.GetAllAsync();

            return challenges.Select(c => new ChallengeResponseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Points = c.Points
            });
        }

        public async Task<ChallengeResponseDto> GetChallengeByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var challenge = await _challengeRepository.GetByIdAsync(id);

            if (challenge == null)
                return null;

            return new ChallengeResponseDto
            {
                Id = challenge.Id,
                Title = challenge.Title,
                Description = challenge.Description,
                Points = challenge.Points
            };
        }

        public async Task<ChallengeResponseDto> CreateChallengeAsync(CreateChallengeDto createChallengeDto)
        {
            var challenge = new Challenge
            {
                Title = createChallengeDto.Title.Trim(),
                Description = createChallengeDto.Description.Trim(),
                Points = createChallengeDto.Points
            };

            await _challengeRepository.AddAsync(challenge);

            return new ChallengeResponseDto
            {
                Id = challenge.Id,
                Title = challenge.Title,
                Description = challenge.Description,
                Points = challenge.Points
            };
        }

        public async Task<ChallengeResponseDto> UpdateChallengeAsync(int id, UpdateChallengeDto updateChallengeDto)
        {
            if (id <= 0)
                return null;

            var challenge = await _challengeRepository.GetByIdAsync(id);

            if (challenge == null)
                return null;

            challenge.Title = updateChallengeDto.Title.Trim();
            challenge.Description = updateChallengeDto.Description.Trim();
            challenge.Points = updateChallengeDto.Points;

            await _challengeRepository.UpdateAsync(challenge);

            return new ChallengeResponseDto
            {
                Id = challenge.Id,
                Title = challenge.Title,
                Description = challenge.Description,
                Points = challenge.Points
            };
        }

        public async Task<bool> DeleteChallengeAsync(int id)
        {
            if (id <= 0)
                return false;

            var challenge = await _challengeRepository.GetByIdAsync(id);

            if (challenge == null)
                return false;

            await _challengeRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> ChallengeExistsAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _challengeRepository.ExistsAsync(id);
        }
    }
}