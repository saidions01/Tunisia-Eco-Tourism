using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TunisiaEco.Core.Models;
using TunisiaEco.Core.DTOs;
using TunisiaEco.Data;

namespace TunisiaEco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActivitiesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities()
    {
        var activities = await _context.Activities
            .Include(a => a.Destination)
            .ToListAsync();

        var activityDtos = activities.Select(a => new ActivityDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            Difficulty = a.Difficulty.ToString(),
            DurationHours = a.DurationHours,
            Price = a.Price,
            DestinationName = a.Destination?.Name ?? "Unknown",
            DestinationRegion = a.Destination?.Region ?? "Unknown"
        }).ToList();

        return activityDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivity(int id)
    {
        var activity = await _context.Activities
            .Include(a => a.Destination)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (activity == null)
        {
            return NotFound();
        }

        var activityDto = new ActivityDto
        {
            Id = activity.Id,
            Name = activity.Name,
            Description = activity.Description,
            Difficulty = activity.Difficulty.ToString(),
            DurationHours = activity.DurationHours,
            Price = activity.Price,
            DestinationName = activity.Destination?.Name ?? "Unknown",
            DestinationRegion = activity.Destination?.Region ?? "Unknown"
        };

        return activityDto;
    }

    [HttpGet("destination/{destinationId}")]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivitiesByDestination(int destinationId)
    {
        var activities = await _context.Activities
            .Include(a => a.Destination)
            .Where(a => a.DestinationId == destinationId)
            .ToListAsync();

        var activityDtos = activities.Select(a => new ActivityDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            Difficulty = a.Difficulty.ToString(),
            DurationHours = a.DurationHours,
            Price = a.Price,
            DestinationName = a.Destination?.Name ?? "Unknown",
            DestinationRegion = a.Destination?.Region ?? "Unknown"
        }).ToList();

        return activityDtos;
    }

    [HttpGet("difficulty/{difficulty}")]
    public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivitiesByDifficulty(string difficulty)
    {
        if (!Enum.TryParse<DifficultyLevel>(difficulty, true, out var difficultyLevel))
        {
            return BadRequest("Invalid difficulty level");
        }

        var activities = await _context.Activities
            .Include(a => a.Destination)
            .Where(a => a.Difficulty == difficultyLevel)
            .ToListAsync();

        var activityDtos = activities.Select(a => new ActivityDto
        {
            Id = a.Id,
            Name = a.Name,
            Description = a.Description,
            Difficulty = a.Difficulty.ToString(),
            DurationHours = a.DurationHours,
            Price = a.Price,
            DestinationName = a.Destination?.Name ?? "Unknown",
            DestinationRegion = a.Destination?.Region ?? "Unknown"
        }).ToList();

        return activityDtos;
    }
}