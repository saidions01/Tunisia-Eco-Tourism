namespace TunisiaEco.Core.Models.Chatbot;

public class ChatResponse
{
    public string Response { get; set; } = string.Empty;
    public string ConversationId { get; set; } = Guid.NewGuid().ToString();
    public List<ChatMessage> History { get; set; } = new();
    public List<EcoRecommendation>? Recommendations { get; set; }
}

public class EcoRecommendation
{
    public string Type { get; set; } = string.Empty; // "destination", "activity", "tip"
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Link { get; set; }
}