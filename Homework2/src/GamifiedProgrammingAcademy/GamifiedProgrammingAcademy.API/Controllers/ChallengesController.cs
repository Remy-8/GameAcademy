using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.API.Data;     
using GamifiedProgrammingAcademy.API.Entities; 
using System.Collections.Generic;              
using System.Threading.Tasks;                  


[ApiController]
[Route("api/[controller]")]
public class ChallengesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ChallengesController(AppDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Challenge>>> GetChallenges()
    {
        return await _context.Challenges.ToListAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<Challenge>> GetChallenge(int id)
    {
        var challenge = await _context.Challenges.FindAsync(id);
        if (challenge == null) return NotFound();
        return challenge;
    }

    
    [HttpPost]
    public async Task<ActionResult<Challenge>> CreateChallenge(Challenge challenge)
    {
        _context.Challenges.Add(challenge);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetChallenge), new { id = challenge.Id }, challenge);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateChallenge(int id, Challenge challenge)
    {
        if (id != challenge.Id) return BadRequest();
        _context.Entry(challenge).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChallenge(int id)
    {
        var challenge = await _context.Challenges.FindAsync(id);
        if (challenge == null) return NotFound();
        _context.Challenges.Remove(challenge);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
