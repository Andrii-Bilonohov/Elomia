namespace Application.Dtos.Requests.Chat;

public sealed record ChatRequestDto(
    Guid UserId,
    string Message
);
        