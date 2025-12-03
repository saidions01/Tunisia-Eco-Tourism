using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TunisiaEco.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreDestinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DurationHours", "Name", "Price" },
                values: new object[] { "Guided tour to observe migratory birds with expert ornithologist", 4, "Bird Watching Tour", 35.00m });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "DestinationId", "Difficulty", "DurationHours", "Name", "Price" },
                values: new object[] { "Learn landscape and wildlife photography techniques", 1, 0, 3, "Nature Photography Workshop", 45.00m });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Description", "DestinationId", "Difficulty", "DurationHours", "Name", "Price" },
                values: new object[,]
                {
                    { 3, "Guided hike through Mediterranean forest trails to Roman aqueducts", 2, 1, 5, "Mountain Hiking Adventure", 40.00m },
                    { 4, "Discover ancient Roman water systems and ruins", 2, 0, 3, "Archaeological Exploration", 25.00m }
                });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestSeason", "Description" },
                values: new object[] { "Winter (November-March)", "UNESCO Biosphere Reserve and World Heritage site known for its lakes and migratory birds including flamingos, storks, and ducks. One of the last remaining freshwater lakes in North Africa." });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestSeason", "Description" },
                values: new object[] { "Spring (March-May)", "Mountain reserve with Roman aqueducts, diverse Mediterranean flora, and hiking trails offering panoramic views. Known as the 'Water Castle' for its springs that supplied ancient Carthage." });

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "BestSeason", "Category", "ConservationStatus", "Description", "ImageUrl", "Latitude", "Longitude", "Name", "Region" },
                values: new object[,]
                {
                    { 3, "All Year", 3, 1, "Natural hot springs by the Mediterranean sea, known for therapeutic properties since Roman times. Stunning coastal cliffs and hiking paths.", "/images/korbous.jpg", 36.8167m, 10.5667m, "Korbous Thermal Springs", "Nabeul" },
                    { 4, "Spring & Autumn", 2, 0, "Coastal paradise with pristine beaches, fishing villages, and citrus groves. Ideal for bird watching and coastal hikes.", "/images/capbon.jpg", 36.7667m, 10.8333m, "Cap Bon Peninsula", "Nabeul" },
                    { 5, "Winter (October-March)", 4, 2, "Desert park protecting the last populations of scimitar-horned oryx and dorcas gazelle. Saharan ecosystem with rare desert flora.", "/images/siditoui.jpg", 32.6833m, 10.5000m, "Sidi Toui National Park", "Tataouine" },
                    { 6, "Summer (June-September)", 2, 1, "Marine protected area and UNESCO Biosphere Reserve. Steep cliffs, crystal clear waters, and important seabird colonies including Cory's shearwaters.", "/images/zembra.jpg", 37.1333m, 10.8000m, "Zembra Island", "Nabeul" },
                    { 7, "Summer (June-September)", 5, 0, "Cork oak and pine forests in Tunisia's highest mountains. Known as 'Little Switzerland' for its cool climate and hiking trails.", "/images/aindraham.jpg", 36.7750m, 8.6922m, "Ain Draham Forest", "Jendouba" },
                    { 8, "Winter (November-February)", 4, 3, "Vast salt lake in the Sahara desert, famous for its mirages and unique salt formations. Best visited at sunset for spectacular colors.", "/images/chott.jpg", 33.7000m, 8.4000m, "Chott El Jerid", "Tozeur" }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Description", "DestinationId", "Difficulty", "DurationHours", "Name", "Price" },
                values: new object[,]
                {
                    { 5, "Natural hot spring bathing with therapeutic benefits", 3, 0, 2, "Thermal Bath Experience", 20.00m },
                    { 6, "Scenic hike along Mediterranean cliffs", 3, 1, 4, "Coastal Cliff Hiking", 30.00m },
                    { 7, "Kayak along pristine coastline with marine life observation", 4, 1, 3, "Sea Kayaking Adventure", 50.00m },
                    { 8, "Learn traditional Tunisian fishing methods with local fishermen", 4, 0, 4, "Traditional Fishing Experience", 35.00m },
                    { 9, "4x4 tour to observe desert wildlife including oryx and gazelles", 5, 1, 6, "Desert Safari", 75.00m },
                    { 10, "Stargazing in the Sahara with astronomer guide", 5, 0, 2, "Desert Night Sky Observation", 30.00m },
                    { 11, "Explore marine biodiversity in protected waters", 6, 0, 3, "Snorkeling Expedition", 45.00m },
                    { 12, "Boat tour to observe rare seabird colonies", 6, 0, 4, "Seabird Watching", 55.00m },
                    { 13, "Suspended walkway through cork oak forest canopy", 7, 1, 3, "Forest Canopy Walk", 35.00m },
                    { 14, "Guided mountain bike trails through pine forests", 7, 2, 4, "Mountain Biking", 40.00m },
                    { 15, "Evening tour to experience spectacular sunset colors on salt lake", 8, 0, 2, "Salt Lake Sunset Tour", 25.00m },
                    { 16, "Professional photography tour capturing desert landscapes", 8, 1, 5, "Desert Photography Expedition", 60.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DurationHours", "Name", "Price" },
                values: new object[] { "Observe migratory birds including flamingos and ducks", 3, "Bird Watching", 25.00m });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "DestinationId", "Difficulty", "DurationHours", "Name", "Price" },
                values: new object[] { "Guided hike through Mediterranean forest trails", 2, 1, 4, "Mountain Hiking", 40.00m });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BestSeason", "Description" },
                values: new object[] { "Winter", "UNESCO Biosphere Reserve known for its lakes and migratory birds" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BestSeason", "Description" },
                values: new object[] { "Spring", "Mountain reserve with Roman aqueducts and diverse flora" });
        }
    }
}
