namespace TunisiaEco.Core.Models;

public class Destination
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string BestSeason { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public EcoCategory Category { get; set; }
    public List<Activity> Activities { get; set; } = new();
    public ConservationStatus ConservationStatus { get; set; }
}

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DifficultyLevel Difficulty { get; set; }
    public int DurationHours { get; set; }
    public decimal Price { get; set; }
    public int DestinationId { get; set; }
    public Destination? Destination { get; set; }
}

public enum EcoCategory
{
    NationalPark,
    BiosphereReserve,
    MarineProtectedArea,
    CulturalHeritage,
    DesertOasis,
    MountainEscape,
    
}

public enum DifficultyLevel
{
    Easy,
    Moderate,
    Challenging
}

public enum ConservationStatus
{
    WellPreserved,
    Protected,
    Endangered,
    RestorationInProgress
}