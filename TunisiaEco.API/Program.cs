using Microsoft.EntityFrameworkCore;
using TunisiaEco.Data;
using TunisiaEco.API.Services;

var builder = WebApplication.CreateBuilder(args);

// HARDCODED connection string for XAMPP
var connectionString = "Server=localhost;Port=3306;Database=tunisiaecotourism;Uid=root;Pwd=;";
Console.WriteLine("Using connection string: " + connectionString);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MySQL Database Configuration - FIXED migrations assembly
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 21)),
        b => b.MigrationsAssembly("TunisiaEco.Data") // Changed to Data project
    ));

// Add Chatbot service
builder.Services.AddScoped<IChatbotService, ChatbotService>();

// Add HTTP client for Llama API
builder.Services.AddHttpClient();

// CORS for frontend communication (including chatbot)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Build the app
var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();