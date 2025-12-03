using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TunisiaEco.Core.Models;
using TunisiaEco.Core.DTOs;
using TunisiaEco.Data;

namespace TunisiaEco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DestinationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DestinationDto>>> GetDestinations()
    {
        var destinations = await _context.Destinations
            .Include(d => d.Activities)
            .ToListAsync();

        var destinationDtos = destinations.Select(d => new DestinationDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Region = d.Region,
            Latitude = d.Latitude,
            Longitude = d.Longitude,
            BestSeason = d.BestSeason,
            ImageUrl = d.ImageUrl,
            Category = d.Category.ToString(),
            ConservationStatus = d.ConservationStatus.ToString(),
            Activities = d.Activities.Select(a => new ActivityDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Difficulty = a.Difficulty.ToString(),
                DurationHours = a.DurationHours,
                Price = a.Price
            }).ToList()
        }).ToList();

        return destinationDtos;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DestinationDto>> GetDestination(int id)
    {
        var destination = await _context.Destinations
            .Include(d => d.Activities)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (destination == null)
        {
            return NotFound();
        }

        var destinationDto = new DestinationDto
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            Region = destination.Region,
            Latitude = destination.Latitude,
            Longitude = destination.Longitude,
            BestSeason = destination.BestSeason,
            ImageUrl = destination.ImageUrl,
            Category = destination.Category.ToString(),
            ConservationStatus = destination.ConservationStatus.ToString(),
            Activities = destination.Activities.Select(a => new ActivityDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Difficulty = a.Difficulty.ToString(),
                DurationHours = a.DurationHours,
                Price = a.Price
            }).ToList()
        };

        return destinationDto;
    }

    [HttpGet("region/{region}")]
    public async Task<ActionResult<IEnumerable<DestinationDto>>> GetDestinationsByRegion(string region)
    {
        var destinations = await _context.Destinations
            .Include(d => d.Activities)
            .Where(d => d.Region == region)
            .ToListAsync();

        var destinationDtos = destinations.Select(d => new DestinationDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Region = d.Region,
            Latitude = d.Latitude,
            Longitude = d.Longitude,
            BestSeason = d.BestSeason,
            ImageUrl = d.ImageUrl,
            Category = d.Category.ToString(),
            ConservationStatus = d.ConservationStatus.ToString(),
            Activities = d.Activities.Select(a => new ActivityDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Difficulty = a.Difficulty.ToString(),
                DurationHours = a.DurationHours,
                Price = a.Price
            }).ToList()
        }).ToList();

        return destinationDtos;
    }
}