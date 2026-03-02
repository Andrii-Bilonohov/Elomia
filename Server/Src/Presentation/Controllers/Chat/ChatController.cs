using Application.Abstractions.Services.Chat;
using Application.Dtos.Requests.Chat;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Chat;

[ApiController ]
[Route("chat")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
    }

    [HttpPost("answer")]
    public async Task<IActionResult> GetAnswerAsync(ChatRequestDto chatRequest, CancellationToken ct = default)
    {
        var response = await _chatService.GetAnswerAsync(chatRequest, ct);
        
        return response.Success ? Ok(response) : BadRequest(response);
    }
}