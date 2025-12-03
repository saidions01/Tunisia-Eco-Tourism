namespace TunisiaEco.Core.DTOs;

public class DestinationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string BestSeason { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string ConservationStatus { get; set; } = string.Empty;
    public List<ActivityDto> Activities { get; set; } = new();
}

public class ActivityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public int DurationHours { get; set; }
    public decimal Price { get; set; }
    
    // Additional properties for activities page
    public string DestinationName { get; set; } = string.Empty;
    public string DestinationRegion { get; set; } = string.Empty;
}