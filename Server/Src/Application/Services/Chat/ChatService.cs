using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Ai;
using Application.Abstractions.Services.Chat;
using Application.Abstractions.Services.UnitOfWork;
using Application.Contracts;
using Application.Dtos.Requests.Chat;
using Application.Dtos.Responses.Chat;
using Domain.Enums;
using Domain.Models;

namespace Application.Services.Chat;

public class ChatService : IChatService
{
    private readonly IAiService _aiService;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(IAiService aiService, IChatMessageRepository chatMessageRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _aiService = aiService;
        _chatMessageRepository = chatMessageRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ChatResponseDto>> GetAnswerAsync(ChatRequestDto request, CancellationToken ct = default)
    {
        try
        {
            if (request is null)
                return new Response<ChatResponseDto>(Error: "Request is null.");

            if (request.UserId == Guid.Empty)
                return new Response<ChatResponseDto>(Error: "UserId is required.");

            if (string.IsNullOrWhiteSpace(request.Message))
                return new Response<ChatResponseDto>(Error: "Message is required.");

            var userExists = await _userRepository.ExistsAsync(request.UserId, ct);
            if (!userExists)
                return new Response<ChatResponseDto>(Error: "User not found.");

            var userMessage = new ChatMessage
            {
                UserId = request.UserId,
                Role = ChatRole.User,
                Text = request.Message
            };

            _chatMessageRepository.Add(userMessage);
            await _unitOfWork.SaveChangesAsync(ct);

            var history = await _chatMessageRepository.GetRecentByUserIdAsync(request.UserId, 10, ct);

            var prompt = BuildPrompt(history);

            var aiAnswer = await _aiService.GetAnswerAsync(prompt, ct);

            var assistantMessage = new ChatMessage
            {
                UserId = request.UserId,
                Role = ChatRole.Assistant,
                Text = aiAnswer
            };

            _chatMessageRepository.Add(assistantMessage);
            await _unitOfWork.SaveChangesAsync(ct);

            return new Response<ChatResponseDto>(
                Result: new ChatResponseDto(aiAnswer));
        }
        catch
        {
            return new Response<ChatResponseDto>(
                Error: "Failed to generate AI response.");
        }
    }

    private static string BuildPrompt(IReadOnlyList<ChatMessage> history)
    {
        var lines = new List<string>
        {
            "You are a supportive AI psychologist assistant.",
            "Be calm, empathetic, and concise.",
            "Do not diagnose medical conditions.",
            "Use the chat history below to answer consistently.",
            "",
            "Chat history:"
        };

        foreach (var message in history)
        {
            var role = message.Role switch
            {
                ChatRole.User => "User",
                ChatRole.Assistant => "Assistant",
                ChatRole.System => "System",
                _ => "Unknown"
            };

            lines.Add($"{role}: {message.Text}");
        }

        lines.Add("");
        lines.Add("Reply to the last user message.");

        return string.Join(Environment.NewLine, lines);
    }
}