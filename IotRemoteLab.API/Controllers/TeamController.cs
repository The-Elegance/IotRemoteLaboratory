using Asp.Versioning;
using IotRemoteLab.Application;
using IotRemoteLab.Domain;
using IotRemoteLab.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IotRemoteLab.API.Controllers;


[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ApplicationContext _dbContext;

    public TeamController(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam(Team team)
    {
        try
        {
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return Ok();
    }


    [HttpGet("{id:guid}")]
    public Task<Team?> Team([FromRoute] Guid id)
    {
        return _dbContext.Teams
            .Include(t => t.Members)
            .FirstOrDefaultAsync(team => team.Id == id);
    }

    [HttpPut]
    public async Task<IActionResult> EditTeam(Team team) 
    {
        try 
        {
            _dbContext.Teams.Update(team);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTeam([FromRoute] Guid id)
    {
        try 
        {
            _dbContext.Remove(id);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost("member/{teamId:guid}/{userId:guid}")]
    public async Task<IActionResult> AddMember([FromRoute] Guid teamId, [FromRoute] Guid userId)
    {
        try
        {
            var targetTeam = await _dbContext.Teams
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (targetTeam == null) 
            {
                return NotFound($"Team with ID - {teamId} not found");
            }

            var targetUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (targetUser == null)
            {
                return NotFound($"User with ID - {userId} not found");
            }

            targetTeam.Members.Add(targetUser);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return Ok();
    }

    [HttpDelete("member/{teamId:guid}/{userId:guid}")]
    public async Task<IActionResult> DeleteMember([FromRoute] Guid teamId, [FromRoute] Guid userId)
    {
        try
        {
            var targetTeam = await _dbContext.Teams
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (targetTeam == null)
            {
                return NotFound($"Team with ID - {teamId} not found");
            }

            var targetUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (targetUser == null)
            {
                return NotFound($"User with ID - {userId} not found");
            }

            targetTeam.Members.Remove(targetUser);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

        return Ok();
    }
}