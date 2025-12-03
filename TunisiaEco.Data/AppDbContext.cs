using Microsoft.EntityFrameworkCore;
using TunisiaEco.Core.Models;

namespace TunisiaEco.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Destination> Destinations { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Your existing seed data...
        modelBuilder.Entity<Destination>().HasData(
          
            new Destination
        {
            Id = 1,
            Name = "Ichkeul National Park",
            Description = "UNESCO Biosphere Reserve and World Heritage site known for its lakes and migratory birds including flamingos, storks, and ducks. One of the last remaining freshwater lakes in North Africa.",
            Region = "Bizerte",
            Latitude = 37.1625m,
            Longitude = 9.6647m,
            BestSeason = "Winter (November-March)",
            Category = EcoCategory.NationalPark,
            ConservationStatus = ConservationStatus.Protected,
            ImageUrl = "/images/ichkeul.jpg"
        },
        new Destination
        {
            Id = 2,
            Name = "Djebel Zaghouan",
            Description = "Mountain reserve with Roman aqueducts, diverse Mediterranean flora, and hiking trails offering panoramic views. Known as the 'Water Castle' for its springs that supplied ancient Carthage.",
            Region = "Zaghouan",
            Latitude = 36.4028m,
            Longitude = 10.1425m,
            BestSeason = "Spring (March-May)",
            Category = EcoCategory.MountainEscape,
            ConservationStatus = ConservationStatus.WellPreserved,
            ImageUrl = "/images/zaghouan.jpg"
        },
        new Destination
        {
            Id = 3,
            Name = "Korbous Thermal Springs",
            Description = "Natural hot springs by the Mediterranean sea, known for therapeutic properties since Roman times. Stunning coastal cliffs and hiking paths.",
            Region = "Nabeul",
            Latitude = 36.8167m,
            Longitude = 10.5667m,
            BestSeason = "All Year",
            Category = EcoCategory.CulturalHeritage,
            ConservationStatus = ConservationStatus.Protected,
            ImageUrl = "/images/korbous.jpg"
        },
        new Destination
        {
            Id = 4,
            Name = "Cap Bon Peninsula",
            Description = "Coastal paradise with pristine beaches, fishing villages, and citrus groves. Ideal for bird watching and coastal hikes.",
            Region = "Nabeul",
            Latitude = 36.7667m,
            Longitude = 10.8333m,
            BestSeason = "Spring & Autumn",
            Category = EcoCategory.MarineProtectedArea,
            ConservationStatus = ConservationStatus.WellPreserved,
            ImageUrl = "/images/capbon.jpg"
        },
        new Destination
        {
            Id = 5,
            Name = "Sidi Toui National Park",
            Description = "Desert park protecting the last populations of scimitar-horned oryx and dorcas gazelle. Saharan ecosystem with rare desert flora.",
            Region = "Tataouine",
            Latitude = 32.6833m,
            Longitude = 10.5000m,
            BestSeason = "Winter (October-March)",
            Category = EcoCategory.DesertOasis,
            ConservationStatus = ConservationStatus.Endangered,
            ImageUrl = "/images/siditoui.jpg"
        },
        new Destination
        {
            Id = 6,
            Name = "Zembra Island",
            Description = "Marine protected area and UNESCO Biosphere Reserve. Steep cliffs, crystal clear waters, and important seabird colonies including Cory's shearwaters.",
            Region = "Nabeul",
            Latitude = 37.1333m,
            Longitude = 10.8000m,
            BestSeason = "Summer (June-September)",
            Category = EcoCategory.MarineProtectedArea,
            ConservationStatus = ConservationStatus.Protected,
            ImageUrl = "/images/zembra.jpg"
        },
        new Destination
        {
            Id = 7,
            Name = "Ain Draham Forest",
            Description = "Cork oak and pine forests in Tunisia's highest mountains. Known as 'Little Switzerland' for its cool climate and hiking trails.",
            Region = "Jendouba",
            Latitude = 36.7750m,
            Longitude = 8.6922m,
            BestSeason = "Summer (June-September)",
            Category = EcoCategory.MountainEscape,
            ConservationStatus = ConservationStatus.WellPreserved,
            ImageUrl = "/images/aindraham.jpg"
        },
        new Destination
        {
            Id = 8,
            Name = "Chott El Jerid",
            Description = "Vast salt lake in the Sahara desert, famous for its mirages and unique salt formations. Best visited at sunset for spectacular colors.",
            Region = "Tozeur",
            Latitude = 33.7000m,
            Longitude = 8.4000m,
            BestSeason = "Winter (November-February)",
            Category = EcoCategory.DesertOasis,
            ConservationStatus = ConservationStatus.RestorationInProgress,
            ImageUrl = "/images/chott.jpg"
        }
    );

    // Seed data for activities
    modelBuilder.Entity<Activity>().HasData(
        // Activities for Ichkeul National Park
        new Activity
        {
            Id = 1,
            Name = "Bird Watching Tour",
            Description = "Guided tour to observe migratory birds with expert ornithologist",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 4,
            Price = 35.00m,
            DestinationId = 1
        },
        new Activity
        {
            Id = 2,
            Name = "Nature Photography Workshop",
            Description = "Learn landscape and wildlife photography techniques",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 3,
            Price = 45.00m,
            DestinationId = 1
        },

        // Activities for Djebel Zaghouan
        new Activity
        {
            Id = 3,
            Name = "Mountain Hiking Adventure",
            Description = "Guided hike through Mediterranean forest trails to Roman aqueducts",
            Difficulty = DifficultyLevel.Moderate,
            DurationHours = 5,
            Price = 40.00m,
            DestinationId = 2
        },
        new Activity
        {
            Id = 4,
            Name = "Archaeological Exploration",
            Description = "Discover ancient Roman water systems and ruins",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 3,
            Price = 25.00m,
            DestinationId = 2
        },

        // Activities for Korbous Thermal Springs
        new Activity
        {
            Id = 5,
            Name = "Thermal Bath Experience",
            Description = "Natural hot spring bathing with therapeutic benefits",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 2,
            Price = 20.00m,
            DestinationId = 3
        },
        new Activity
        {
            Id = 6,
            Name = "Coastal Cliff Hiking",
            Description = "Scenic hike along Mediterranean cliffs",
            Difficulty = DifficultyLevel.Moderate,
            DurationHours = 4,
            Price = 30.00m,
            DestinationId = 3
        },

        // Activities for Cap Bon Peninsula
        new Activity
        {
            Id = 7,
            Name = "Sea Kayaking Adventure",
            Description = "Kayak along pristine coastline with marine life observation",
            Difficulty = DifficultyLevel.Moderate,
            DurationHours = 3,
            Price = 50.00m,
            DestinationId = 4
        },
        new Activity
        {
            Id = 8,
            Name = "Traditional Fishing Experience",
            Description = "Learn traditional Tunisian fishing methods with local fishermen",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 4,
            Price = 35.00m,
            DestinationId = 4
        },

        // Activities for Sidi Toui National Park
        new Activity
        {
            Id = 9,
            Name = "Desert Safari",
            Description = "4x4 tour to observe desert wildlife including oryx and gazelles",
            Difficulty = DifficultyLevel.Moderate,
            DurationHours = 6,
            Price = 75.00m,
            DestinationId = 5
        },
        new Activity
        {
            Id = 10,
            Name = "Desert Night Sky Observation",
            Description = "Stargazing in the Sahara with astronomer guide",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 2,
            Price = 30.00m,
            DestinationId = 5
        },

        // Activities for Zembra Island
        new Activity
        {
            Id = 11,
            Name = "Snorkeling Expedition",
            Description = "Explore marine biodiversity in protected waters",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 3,
            Price = 45.00m,
            DestinationId = 6
        },
        new Activity
        {
            Id = 12,
            Name = "Seabird Watching",
            Description = "Boat tour to observe rare seabird colonies",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 4,
            Price = 55.00m,
            DestinationId = 6
        },

        // Activities for Ain Draham Forest
        new Activity
        {
            Id = 13,
            Name = "Forest Canopy Walk",
            Description = "Suspended walkway through cork oak forest canopy",
            Difficulty = DifficultyLevel.Moderate,
            DurationHours = 3,
            Price = 35.00m,
            DestinationId = 7
        },
        new Activity
        {
            Id = 14,
            Name = "Mountain Biking",
            Description = "Guided mountain bike trails through pine forests",
            Difficulty = DifficultyLevel.Challenging,
            DurationHours = 4,
            Price = 40.00m,
            DestinationId = 7
        },

        // Activities for Chott El Jerid
        new Activity
        {
            Id = 15,
            Name = "Salt Lake Sunset Tour",
            Description = "Evening tour to experience spectacular sunset colors on salt lake",
            Difficulty = DifficultyLevel.Easy,
            DurationHours = 2,
            Price = 25.00m,
            DestinationId = 8
        },
        new Activity
        {
            Id = 16,
            Name = "Desert Photography Expedition",
            Description = "Professional photography tour capturing desert landscapes",
            Difficulty = DifficultyLevel.Moderate,
            DurationHours = 5,
            Price = 60.00m,
            DestinationId = 8
        }
    );
    
    
    }
}
