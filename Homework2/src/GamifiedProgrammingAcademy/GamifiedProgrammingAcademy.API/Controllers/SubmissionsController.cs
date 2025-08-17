#nullable disable
using Microsoft.AspNetCore.Mvc;
using GamifiedProgrammingAcademy.API.Contracts;
using GamifiedProgrammingAcademy.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamifiedProgrammingAcademy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmissionResponseDto>>> GetSubmissions()
        {
            var submissions = await _submissionService.GetAllSubmissionsAsync();
            return Ok(submissions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubmissionResponseDto>> GetSubmission(int id)
        {
            var submission = await _submissionService.GetSubmissionByIdAsync(id);

            if (submission == null)
                return NotFound($"No se encontró la submission con ID {id}");

            return Ok(submission);
        }

        [HttpPost]
        public async Task<ActionResult<SubmissionResponseDto>> CreateSubmission(CreateSubmissionDto createSubmissionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var submission = await _submissionService.CreateSubmissionAsync(createSubmissionDto);
                return CreatedAtAction(nameof(GetSubmission), new { id = submission.Id }, submission);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SubmissionResponseDto>>> GetUserSubmissions(int userId)
        {
            var submissions = await _submissionService.GetUserSubmissionsAsync(userId);
            return Ok(submissions);
        }

        [HttpGet("challenge/{challengeId}")]
        public async Task<ActionResult<IEnumerable<SubmissionResponseDto>>> GetChallengeSubmissions(int challengeId)
        {
            var submissions = await _submissionService.GetChallengeSubmissionsAsync(challengeId);
            return Ok(submissions);
        }
    }
}