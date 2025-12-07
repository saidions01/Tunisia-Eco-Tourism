using Microsoft.AspNetCore.Mvc;
using TunisiaEco.API.Services;
using TunisiaEco.Core.Models.Chatbot;

namespace TunisiaEco.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatbotController : ControllerBase
{
    private readonly IChatbotService _chatbotService;

    public ChatbotController(IChatbotService chatbotService)
    {
        _chatbotService = chatbotService;
    }

    [HttpPost("chat")]
    public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatRequest request)
    {
        try
        {
            var response = await _chatbotService.ProcessMessageAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("welcome")]
    public ActionResult<ChatResponse> GetWelcomeMessage()
    {
        var response = new ChatResponse
        {
            Response = "👋 Welcome to ETNAFES! I'm EcoGuide, your Tunisian eco-tourism assistant. Ask me about destinations, activities, or sustainable travel tips!",
            Recommendations = new List<EcoRecommendation>
            {
                new() { Type = "tip", Name = "Quick Start", Description = "Try asking: 'What are the best eco-destinations in Tunisia?' or 'Tell me about sustainable activities'" }
            }
        };
        
        return Ok(response);
    }
}