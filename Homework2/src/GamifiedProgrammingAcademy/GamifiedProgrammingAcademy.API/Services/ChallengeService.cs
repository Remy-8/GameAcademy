using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Data;
using GamifiedProgrammingAcademy.API.Dtos;
using GamifiedProgrammingAcademy.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly AppDbContext _context;

        public ChallengeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChallengeResponseDto>> GetAllChallengesAsync()
        {
            var challenges = await _context.Challenges.ToListAsync();

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
            // Validar que el ID sea válido
            if (id <= 0)
                return null;

            var challenge = await _context.Challenges.FindAsync(id);

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
            // Validaciones adicionales de negocio
            if (await TitleAlreadyExistsAsync(createChallengeDto.Title))
                throw new InvalidOperationException("Ya existe un desafío con ese título");

            var challenge = new Challenge
            {
                Title = createChallengeDto.Title.Trim(),
                Description = createChallengeDto.Description.Trim(),
                Points = createChallengeDto.Points
            };

            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();

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
            // Validar que el ID sea válido
            if (id <= 0)
                return null;

            var challenge = await _context.Challenges.FindAsync(id);

            if (challenge == null)
                return null;

            // Validar que el título no esté duplicado (excepto el actual)
            if (await TitleAlreadyExistsAsync(updateChallengeDto.Title, id))
                throw new InvalidOperationException("Ya existe un desafío con ese título");

            // Actualizar propiedades
            challenge.Title = updateChallengeDto.Title.Trim();
            challenge.Description = updateChallengeDto.Description.Trim();
            challenge.Points = updateChallengeDto.Points;

            _context.Entry(challenge).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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
            // Validar que ID sea valido
            if (id <= 0)
                return false;

            var challenge = await _context.Challenges.FindAsync(id);

            if (challenge == null)
                return false;

            _context.Challenges.Remove(challenge);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChallengeExistsAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _context.Challenges.AnyAsync(c => c.Id == id);
        }

        // Métodos privados para validaciones de negocio
        private async Task<bool> TitleAlreadyExistsAsync(string title, int? excludeId = null)
        {
            var query = _context.Challenges.Where(c => c.Title.ToLower() == title.ToLower());

            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}