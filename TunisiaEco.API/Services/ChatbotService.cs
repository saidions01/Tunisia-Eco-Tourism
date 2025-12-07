using TunisiaEco.Core.Models.Chatbot;
using TunisiaEco.Core.Models;

using TunisiaEco.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace TunisiaEco.API.Services;

public interface IChatbotService
{
    Task<ChatResponse> ProcessMessageAsync(ChatRequest request);
    Task<string> GenerateEcoResponseAsync(string userMessage, List<ChatMessage>? history = null);
    Task<List<EcoRecommendation>> GetRecommendationsAsync(string userMessage);
}

public class ChatbotService : IChatbotService
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    // Simple knowledge base for Tunisian eco-tourism
    private readonly Dictionary<string, string[]> _ecoKnowledge = new()
    {
        ["greeting"] = new[] {
            "Hello! I'm EcoGuide, your Tunisian eco-tourism assistant. How can I help you plan your sustainable adventure? 🌿",
            "Welcome to ETNAFES! I'm here to help you discover Tunisia's natural wonders responsibly.",
            "Hi there! Ready to explore Tunisia's eco-destinations? Ask me about national parks, activities, or conservation efforts!"
        },
        
        ["destinations"] = new[] {
            "Tunisia has amazing eco-destinations! 🗺️ Let me recommend some:",
            "Here are some sustainable destinations you might like:",
            "Based on your interests, consider these eco-friendly spots:"
        },
        
        ["activities"] = new[] {
            "Great choice! Here are eco-activities available: 🚵‍♂️",
            "Sustainable activities in Tunisia include:",
            "For an authentic eco-experience, try these activities:"
        },
        
        ["conservation"] = new[] {
            "Conservation is key! Tunisia has several protected areas and initiatives: 🌱",
            "We're committed to preserving Tunisia's natural heritage through:",
            "Sustainable tourism helps protect our ecosystems. Here's how you can contribute:"
        },
        
        ["season"] = new[] {
            "The best time depends on what you want to experience! 🗓️",
            "Tunisia's climate varies by region. Here's a seasonal guide:",
            "Planning your visit? Consider these seasonal recommendations:"
        },
        
        ["unknown"] = new[] {
            "I'm still learning about Tunisian eco-tourism. Could you rephrase your question? 🤔",
            "For detailed information about specific destinations or activities, please check our Destinations or Activities pages.",
            "I specialize in Tunisian eco-tourism. Try asking about national parks, sustainable activities, or best seasons to visit!"
        }
    };

    public ChatbotService(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<ChatResponse> ProcessMessageAsync(ChatRequest request)
    {
        var response = new ChatResponse
        {
            ConversationId = request.ConversationId ?? Guid.NewGuid().ToString()
        };

        try
        {
            // Get recommendations from database
            var recommendations = await GetRecommendationsAsync(request.Message);
            
            // Generate response
            string chatbotResponse;
            
            if (_configuration.GetValue<bool>("Chatbot:UseLlama") && 
                !string.IsNullOrEmpty(_configuration["Chatbot:LlamaApiUrl"]))
            {
                // Use Llama API if configured
                chatbotResponse = await GenerateLlamaResponseAsync(request.Message);
            }
            else
            {
                // Use our knowledge base
                chatbotResponse = await GenerateEcoResponseAsync(request.Message, request.History);
            }
            
            response.Response = chatbotResponse;
            response.Recommendations = recommendations;
            
            // Build conversation history
            var history = request.History ?? new List<ChatMessage>();
            history.Add(new ChatMessage { Role = "user", Content = request.Message });
            history.Add(new ChatMessage { Role = "assistant", Content = chatbotResponse });
            response.History = history.TakeLast(10).ToList(); // Keep last 10 messages
        }
        catch (Exception ex)
        {
            response.Response = $"I encountered an issue. Please try again. Error: {ex.Message}";
        }

        return response;
    }

    public async Task<string> GenerateEcoResponseAsync(string userMessage, List<ChatMessage>? history = null)
    {
        var message = userMessage.ToLower();
        var response = new StringBuilder();

        // Check for keywords and provide relevant responses
        if (ContainsAny(message, new[] { "hello", "hi", "hey", "bonjour", "salam" }))
        {
            response.AppendLine(GetRandomResponse("greeting"));
        }
        
        if (ContainsAny(message, new[] { "destination", "place", "park", "reserve", "where to go" }))
        {
            response.AppendLine(GetRandomResponse("destinations"));
            
            // Add actual destinations from database
            var destinations = await _context.Destinations.Take(3).ToListAsync();
            foreach (var dest in destinations)
            {
                response.AppendLine($"• **{dest.Name}** ({dest.Region}): {dest.Description.Substring(0, Math.Min(100, dest.Description.Length))}...");
            }
            response.AppendLine("Check our Destinations page for more details!");
        }
        
        if (ContainsAny(message, new[] { "activity", "do", "experience", "hiking", "bird", "swim" }))
        {
            response.AppendLine(GetRandomResponse("activities"));
            
            var activities = await _context.Activities
                .Include(a => a.Destination)
                .Take(3)
                .ToListAsync();
                
            foreach (var activity in activities)
            {
                response.AppendLine($"• **{activity.Name}** at {activity.Destination?.Name}: {activity.DurationHours} hours, ${activity.Price}");
            }
            response.AppendLine("See all activities on our Activities page!");
        }
        
        if (ContainsAny(message, new[] { "conservation", "protect", "sustainable", "eco", "green" }))
        {
            response.AppendLine(GetRandomResponse("conservation"));
            response.AppendLine("• Support local conservation projects");
            response.AppendLine("• Respect wildlife and natural habitats");
            response.AppendLine("• Use eco-friendly transportation");
            response.AppendLine("• Reduce plastic usage during your visit");
        }
        
        if (ContainsAny(message, new[] { "season", "when", "best time", "weather", "climate" }))
        {
            response.AppendLine(GetRandomResponse("season"));
            response.AppendLine("• **Spring (Mar-May)**: Perfect for hiking and wildflowers");
            response.AppendLine("• **Summer (Jun-Aug)**: Great for coastal activities");
            response.AppendLine("• **Autumn (Sep-Nov)**: Ideal for bird watching");
            response.AppendLine("• **Winter (Dec-Feb)**: Best for desert exploration");
        }
        
        // If no specific category matched
        if (response.Length == 0)
        {
            response.AppendLine(GetRandomResponse("unknown"));
            response.AppendLine("You can ask me about:");
            response.AppendLine("• Tunisian national parks");
            response.AppendLine("• Eco-friendly activities");
            response.AppendLine("• Best seasons to visit");
            response.AppendLine("• Conservation efforts");
        }

        return response.ToString();
    }

   public async Task<List<EcoRecommendation>> GetRecommendationsAsync(string userMessage)
{
    var recommendations = new List<EcoRecommendation>();
    var message = userMessage.ToLower();

    // Match keywords to destinations
    if (ContainsAny(message, new[] { "bird", "lake", "water" }))
    {
        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.Name.Contains("Ichkeul"));
        if (destination != null)
        {
            recommendations.Add(new EcoRecommendation
            {
                Type = "destination",
                Name = destination.Name,
                Description = destination.Description.Substring(0, Math.Min(100, destination.Description.Length)) + "...",
                Link = $"/destinations"
            });
        }
    }

    if (ContainsAny(message, new[] { "mountain", "hike", "forest" }))
    {
        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.Name.Contains("Zaghouan") || d.Name.Contains("Ain Draham"));
        if (destination != null)
        {
            recommendations.Add(new EcoRecommendation
            {
                Type = "destination",
                Name = destination.Name,
                Description = $"Best for {destination.BestSeason}: {destination.Description.Substring(0, Math.Min(100, destination.Description.Length))}...",
                Link = $"/destinations"
            });
        }
    }

    if (ContainsAny(message, new[] { "desert", "sand", "sahara" }))
    {
        var destination = await _context.Destinations
            .FirstOrDefaultAsync(d => d.Category == EcoCategory.DesertOasis); // This line remains unchanged
        if (destination != null)
        {
            recommendations.Add(new EcoRecommendation
            {
                Type = "destination",
                Name = destination.Name,
                Description = destination.Description.Substring(0, Math.Min(100, destination.Description.Length)) + "...",
                Link = $"/destinations"
            });
        }
    }

    return recommendations;
}
    private async Task<string> GenerateLlamaResponseAsync(string userMessage)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();
            var llamaUrl = _configuration["Chatbot:LlamaApiUrl"];
            
            // Prepare prompt with eco-tourism context
            var prompt = $@"You are EcoGuide, an expert on Tunisian eco-tourism. 
            You help tourists plan sustainable trips in Tunisia.
            Focus on national parks, conservation, eco-activities, and responsible tourism.
            
            User: {userMessage}
            
            EcoGuide:";
            
            var request = new
            {
                prompt = prompt,
                max_tokens = 150,
                temperature = 0.7
            };
            
            var response = await httpClient.PostAsJsonAsync(llamaUrl, request);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LlamaResponse>();
                return result?.choices?.FirstOrDefault()?.text?.Trim() ?? 
                       await GenerateEcoResponseAsync(userMessage);
            }
        }
        catch
        {
            // Fall back to knowledge base
        }
        
        return await GenerateEcoResponseAsync(userMessage);
    }

    private string GetRandomResponse(string category)
    {
        if (_ecoKnowledge.TryGetValue(category, out var responses) && responses.Length > 0)
        {
            var random = new Random();
            return responses[random.Next(responses.Length)];
        }
        return "How can I help you with Tunisian eco-tourism?";
    }

    private bool ContainsAny(string text, string[] keywords)
    {
        return keywords.Any(keyword => text.Contains(keyword));
    }

    // Llama API response model
    private class LlamaResponse
    {
        public List<LlamaChoice>? choices { get; set; }
    }

    private class LlamaChoice
    {
        public string? text { get; set; }
    }
}