using Microsoft.EntityFrameworkCore;
using TunisiaEco.Data;
using TunisiaEco.API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => 
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                // HARDCODED connection string for XAMPP
                var connectionString = "Server=localhost;Port=3306;Database=tunisiaecotourism;Uid=root;Pwd=;";
                Console.WriteLine("Using connection string: " + connectionString);

                // Add services
                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen();

                // MySQL Database Configuration - FIXED migrations assembly
                services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(
                        connectionString,
                        new MySqlServerVersion(new Version(8, 0, 21)),
                        b => b.MigrationsAssembly("TunisiaEco.Data") // Changed to Data project
                    ));

                // CORS for frontend communication
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowBlazorApp", policy =>
                    {
                        policy.WithOrigins("*")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
                });

                // Add Chatbot service
                services.AddScoped<IChatbotService, ChatbotService>();

                // Add HTTP client for Llama API
                services.AddHttpClient();

                // Add CORS for chatbot
                services.AddCors(options =>
                {
                    options.AddPolicy("ChatbotPolicy", policy =>
                    {
                        policy.WithOrigins("http://localhost:5001", "https://localhost:7001")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
                });
            });
}
